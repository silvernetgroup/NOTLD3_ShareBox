using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class AlbumsDataSource : DataSourceBase<AlbumsSchema>
    {
        private const string _file = "/Assets/Data/AlbumsDataSource.json";

        protected override string CacheKey
        {
            get { return "AlbumsDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return true; }
        }

        public async override Task<IEnumerable<AlbumsSchema>> LoadDataAsync()
        {
            try
            {
                var serviceDataProvider = new StaticDataProvider(_file);
                return await serviceDataProvider.Load<AlbumsSchema>();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("AlbumsDataSource.LoadData", ex.ToString());
                return new AlbumsSchema[0];
            }
        }
    }
}
