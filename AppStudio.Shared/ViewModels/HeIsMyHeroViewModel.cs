using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class HeIsMyHeroViewModel : ViewModelBase<HtmlSchema>
    {
        private RelayCommandEx<HtmlSchema> itemClickCommand;
        public RelayCommandEx<HtmlSchema> ItemClickCommand
        {
            get
            {
                if (itemClickCommand == null)
                {
                    itemClickCommand = new RelayCommandEx<HtmlSchema>(
                        (item) =>
                        {
                            NavigationServices.NavigateToPage("", item);
                        });
                }

                return itemClickCommand;
            }
        }

        override protected DataSourceBase<HtmlSchema> CreateDataSource()
        {
            return new HeIsMyHeroDataSource(); // HtmlDataSource
        }


        override public ViewTypes ViewType
        {
            get { return ViewTypes.Detail; }
        }

        override public Visibility PinToStartVisibility
        {
            get { return ViewType == ViewTypes.Detail ? Visibility.Visible : Visibility.Collapsed; }
        }

        override protected void PinToStart()
        {
            base.PinToStart("MainPage", @"He is my hero", "{Content}", "");
        }

        override public Visibility ShareItemVisibility
        {
            get { return ViewType == ViewTypes.Detail ? Visibility.Visible : Visibility.Collapsed; }
        }

        override public Visibility TextToSpeechVisibility
        {
            get { return ViewType == ViewTypes.Detail ? Visibility.Visible : Visibility.Collapsed; }
        }

        override protected async void TextToSpeech()
        {
            await base.SpeakText("Content");
        }

        public RelayCommandEx<Slider> IncreaseSlider
        {
            get
            {
                return new RelayCommandEx<Slider>(s => s.Value++);
            }
        }

        public RelayCommandEx<Slider> DecreaseSlider
        {
            get
            {
                return new RelayCommandEx<Slider>(s => s.Value--);
            }
        }
    }
}
