using Project_Pinterest.Entities;
using Project_Pinterest.Payloads.DataResponses.DataPostCollection;

namespace Project_Pinterest.Payloads.DataResponses.DataCollection
{
    public class DataResponseCollection : DataResponseBase
    {
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public IQueryable<DataResponsePostCollection> DataResponsePostCollections { get; set; }
    }
}
