using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cabarles_IPT.CabarlesDomain.Commands;
using Cabarles_IPT.CabarlesDomain.Models;
using Cabarles_IPT.CabarlesFramework.DTOs;

namespace Cabarles_IPT.CabarlesFramework.Commands
{
    public class UpdateYouTubeViewerCommand : IUpdateYouTubeViewerCommand
    {
        private readonly YouTubeViewersDbContextFactory _contextFactory;

        public UpdateYouTubeViewerCommand(YouTubeViewersDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(YouTubeViewer youTubeViewer)
        {
            using (YouTubeViewersDbContext context = _contextFactory.Create())
            {
                YouTubeViewerDto youTubeViewerDto = new YouTubeViewerDto()
                {
                    Id = youTubeViewer.Id,
                    Username = youTubeViewer.Username,
                    IsSubscribed = youTubeViewer.IsSubscribed,
                    IsMember = youTubeViewer.IsMember,
                };

                context.YouTubeViewers.Update(youTubeViewerDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
