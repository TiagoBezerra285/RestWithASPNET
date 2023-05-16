using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Hypermedia.Constants;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Hypermedia.Enricher
{
    public class PersonEnricher : ContentResponseEnricher<PersonVO>
    {
        private readonly object _lock = new object();
        protected override Task EnrichModel(PersonVO context, IUrlHelper urlHelper)
        {
            var path = "api/person/v1";
            string link = GetLink(context.Id, urlHelper, path);

            context.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.GET,
                Href = link,
                Rel = ReletionType.self,
                Type = ResponseTypeFormat.DefaultGet
            });

            context.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.POST,
                Href = link,
                Rel = ReletionType.self,
                Type = ResponseTypeFormat.DefaultPost
            });

            context.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.PUT,
                Href = link,
                Rel = ReletionType.self,
                Type = ResponseTypeFormat.DefaultPut
            });

            context.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.PATCH,
                Href = link,
                Rel = ReletionType.self,
                Type = ResponseTypeFormat.DefaultPatch
            });

            context.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.DELETE,
                Href = link,
                Rel = ReletionType.self,
                Type = "int"
            });

            return null;

        }

        private string GetLink(long id, IUrlHelper urlHelper, object path)
        {
            lock(_lock)
            {
                var url = new { controller = path, id = id };
                return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
            }
        }
    }
}
