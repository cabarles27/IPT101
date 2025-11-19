using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cabarles_IPT.CabarlesDomain.Models;
using Cabarles_IPT.CabarlesYoutubeViewer.Stores;
using Cabarles_IPT.CabarlesYoutubeViewer.ViewModels;

namespace Cabarles_IPT.CabarlesYoutubeViewer.Commands
{
    public class DeleteYouTubeViewerCommand : AsyncCommandBase
    {
        private readonly YouTubeViewersListingItemViewModel _youTubeViewersListingItemViewModel;
        private readonly YouTubeViewersStore _youTubeViewersStore;

        public DeleteYouTubeViewerCommand(YouTubeViewersListingItemViewModel youTubeViewersListingItemViewModel, YouTubeViewersStore youTubeViewersStore)
        {
            _youTubeViewersListingItemViewModel = youTubeViewersListingItemViewModel;
            _youTubeViewersStore = youTubeViewersStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _youTubeViewersListingItemViewModel.ErrorMessage = null;
            _youTubeViewersListingItemViewModel.IsDeleting = true;

            YouTubeViewer youTubeViewer = _youTubeViewersListingItemViewModel.YouTubeViewer;

            try
            {
                await _youTubeViewersStore.Delete(youTubeViewer.Id);
            }
            catch (Exception)
            {
                _youTubeViewersListingItemViewModel.ErrorMessage = "Failed to delete YouTube viewer. Please try again later.";
            }
            finally
            {
                _youTubeViewersListingItemViewModel.IsDeleting = false;
            }
        }
    }
}
