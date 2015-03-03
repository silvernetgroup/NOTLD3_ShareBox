using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class AlbumsViewModel : ViewModelBase<AlbumsSchema>
    {
        private RelayCommandEx<AlbumsSchema> itemClickCommand;
        public RelayCommandEx<AlbumsSchema> ItemClickCommand
        {
            get
            {
                if (itemClickCommand == null)
                {
                    itemClickCommand = new RelayCommandEx<AlbumsSchema>(
                        (item) =>
                        {
                            NavigationServices.NavigateToPage("AlbumsDetail", item);
                        });
                }

                return itemClickCommand;
            }
        }

        override protected DataSourceBase<AlbumsSchema> CreateDataSource()
        {
            return new AlbumsDataSource(); // CollectionDataSource
        }


        override public Visibility PinToStartVisibility
        {
            get { return ViewType == ViewTypes.Detail ? Visibility.Visible : Visibility.Collapsed; }
        }

        override protected void PinToStart()
        {
            base.PinToStart("AlbumsDetail", "{DefaultTitle}", "{DefaultSummary}", "{DefaultImageUrl}");
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
            await base.SpeakText("Description");
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

        override public void NavigateToSectionList()
        {
            NavigationServices.NavigateToPage("AlbumsList");
        }

        override protected void NavigateToSelectedItem()
        {
            NavigationServices.NavigateToPage("AlbumsDetail");
        }
    }
}
