using Microsoft.EntityFrameworkCore;
using Project_Pinterest.DataContexts;
using Project_Pinterest.Entities;
using Project_Pinterest.Payloads.DataResponses.DataCollection;

namespace Project_Pinterest.Payloads.Converters
{
    public class CollectionConverter
    {
        private readonly AppDbContext _context;
        private readonly PostCollectionConverter _converter;


        public CollectionConverter(AppDbContext context, PostCollectionConverter converter)
        {
            _context = context;
            _converter = converter;
        }
        public DataResponseCollection EntityToDTO(Collection collection)
        {
            var collectionItem = _context.collections.Include(x => x.User).Include(x => x.PostCollections).AsNoTracking().SingleOrDefault(x => x.Id == collection.Id);
            return new DataResponseCollection()
            {
                Id = collection.Id,
                Name = collection.Name,
                Title = collection.Title,
                UserName = collectionItem.User.FullName,
                DataResponsePostCollections = collectionItem.PostCollections.Select(x => _converter.EntityToDTO(x)).AsQueryable(),
            };
        }
    }
}
