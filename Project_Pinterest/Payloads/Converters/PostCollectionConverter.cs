using Project_Pinterest.Entities;
using Project_Pinterest.Payloads.DataResponses.DataPostCollection;

namespace Project_Pinterest.Payloads.Converters
{
    public class PostCollectionConverter
    {
        public PostCollectionConverter() { }
        public DataResponsePostCollection EntityToDTO(PostCollection postCollection)
        {
            return new DataResponsePostCollection
            {
                PostId = postCollection.Id,
                Id = postCollection.Id,
            };
        }
    }
}
