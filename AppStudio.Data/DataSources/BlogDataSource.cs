using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class BlogDataSource : DataSourceBase<RssSchema>
    {
        private const string _url =@"http://michael-is-the-best.blog.onet.pl/feed";

        protected override string CacheKey
        {
            get { return "BlogDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return false; }
        }

        public async override Task<IEnumerable<RssSchema>> LoadDataAsync()
        {
            try
            {
                var rssDataProvider = new RssDataProvider(_url);
                return await rssDataProvider.Load();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("BlogDataSourceDataSource.LoadData", ex.ToString());
                return new RssSchema[0];
            }
        }
    }
}
