﻿using MaterialDesign.Icons;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;


namespace Plapp
{
    public static class Components
    {
        public static Grid PhotoFrame(string imageBinding, string isVisibleBinding, string takePhotoCommandBinding)
        {
            return new Grid
            {
                Children =
                    {
                        new Image()
                            .Bind(imageBinding),

                        new Button()
                            .MaterialIcon(MaterialIcon.AddAPhoto, IconSize.Huge)
                            .Bind(VisualElement.IsVisibleProperty, isVisibleBinding)
                            .BindCommand(takePhotoCommandBinding)
                    }
            };
        }
    }
}
