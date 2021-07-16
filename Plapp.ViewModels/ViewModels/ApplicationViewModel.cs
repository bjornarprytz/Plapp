using AutoMapper;
using MediatR;
using Plapp.BusinessLogic;
using Plapp.Core;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.ViewModels
{
    public class ApplicationViewModel : IOViewModel, IApplicationViewModel
    {
        private readonly ObservableCollection<ITopicViewModel> _topics;
        private readonly INavigator _navigator;
        private readonly IMediator _mediator;
        private readonly IQueryFactory<Topic> _topicQueryFactory;
        private readonly ICommandFactory<Topic> _topicCommandFactory;
        private readonly ViewModelFactory<ITopicViewModel> _topicFactory;
        private readonly IMapper _mapper;

        public ApplicationViewModel(
            INavigator navigator,
            IMediator mediator,
            IQueryFactory<Topic> topicQueryFactory,
            ICommandFactory<Topic> topicCommandFactory,
            ViewModelFactory<ITopicViewModel> topicFactory,
            IMapper mapper
            )
        {
            _navigator = navigator;
            _mediator = mediator;
            _topicQueryFactory = topicQueryFactory;
            _topicCommandFactory = topicCommandFactory;
            _topicFactory = topicFactory;
            _mapper = mapper;

            _topics = new ObservableCollection<ITopicViewModel>();
            Topics = new ReadOnlyObservableCollection<ITopicViewModel>(_topics);

            AddTopicCommand = new AsyncCommand(AddTopic, allowsMultipleExecutions: false);
            DeleteTopicCommand = new AsyncCommand<ITopicViewModel>(DeleteTopic, allowsMultipleExecutions: false);
        }

        public ReadOnlyObservableCollection<ITopicViewModel> Topics { get; }
        
        public IAsyncCommand AddTopicCommand { get; private set; }
        public IAsyncCommand<ITopicViewModel> DeleteTopicCommand { get; private set; }

        protected override async Task AutoLoadDataAsync()
        {
            await base.AutoLoadDataAsync();

            var result = await _mediator.Send(_topicQueryFactory.CreateFetchAll());

            if (result.Error)
            {
                return;
            }

            var freshTopics = result.Data;

            _topics.Update(
                freshTopics,
                _mapper,
                () => _topicFactory(),
                (d, v) => d.Id == v.Id);
        }

        private async Task AddTopic()
        {
            var newTopic = _topicFactory();

            _topics.Add(newTopic);

            //var topicData = await _topicService.SaveAsync(_mapper.Map<Topic>(newTopic));
            //_mapper.Map(topicData, newTopic);

            await _navigator.GoToAsync(newTopic);
        }

        private async Task DeleteTopic(ITopicViewModel topic)
        {
            _topics.Remove(topic);

            await _mediator.Send(_topicCommandFactory.CreateDelete(topic.Id));
        }
    }
}
