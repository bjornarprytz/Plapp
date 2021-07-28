using AutoMapper;
using MediatR;
using Plapp.BusinessLogic;
using Plapp.BusinessLogic.Queries;
using Plapp.Core;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.ViewModels
{
    public class ApplicationViewModel : IOViewModel, IApplicationViewModel
    {
        private readonly INavigator _navigator;
        private readonly ViewModelFactory<ITopicViewModel> _topicFactory;
        private readonly ITopicService _topicService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ApplicationViewModel(
            INavigator navigator,
            ViewModelFactory<ITopicViewModel> topicFactory, 
            ITopicService topicService,
            IMapper mapper,
            IMediator mediator
            )
        {
            _navigator = navigator;
            _topicFactory = topicFactory;
            _topicService = topicService;
            _mapper = mapper;
            _mediator = mediator;

            Topics = new ObservableCollection<ITopicViewModel>();

            AddTopicCommand = new AsyncCommand(AddTopic, allowsMultipleExecutions: false);
            DeleteTopicCommand = new AsyncCommand<ITopicViewModel>(DeleteTopic, allowsMultipleExecutions: false);
        }

        public ObservableCollection<ITopicViewModel> Topics { get; }
        
        public IAsyncCommand AddTopicCommand { get; private set; }
        public IAsyncCommand<ITopicViewModel> DeleteTopicCommand { get; private set; }

        protected override async Task AutoLoadDataAsync()
        {
            await base.AutoLoadDataAsync();

            var freshTopicResponse = await _mediator.Send(new GetAllTopicsQuery());

            if (freshTopicResponse.Error)
                freshTopicResponse.Throw();

            var freshTopics = freshTopicResponse.Data;

            Topics.Update(
                freshTopics,
                (v1, v2) => v1.Id == v2.Id);
        }

        private async Task AddTopic()
        {
            var newTopic = _topicFactory();

            Topics.Add(newTopic);

            //var topicData = await _topicService.SaveAsync(_mapper.Map<Topic>(newTopic));
            //_mapper.Map(topicData, newTopic);

            await _navigator.GoToAsync(newTopic);
        }

        private async Task DeleteTopic(ITopicViewModel topic)
        {
            Topics.Remove(topic);
            
            await _topicService.DeleteAsync(_mapper.Map<Topic>(topic));
        }
    }
}
