using finalinternshipproject.Data;
using finalinternshipproject.Dtos.Collection;
using finalinternshipproject.Models;
using finalinternshipproject.Models.Fields;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace finalinternshipproject.Services.Collection
{
    public class CollectionService : ICollectionService
    {
        private readonly DataContext context;
        private readonly IHttpContextAccessor httpContext;
        public CollectionService(DataContext context, IHttpContextAccessor httpContext)
        {
            this.context = context;
            this.httpContext = httpContext;
        }

        private int GetUserId()
        {
            return int.Parse(httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }


        private string placeHolderHandle = "Zv8CXOdkRbSNI7Hy0N11";

        public async Task<ServiceResponse<string>> PostCollection(CollectionPostDto request)
        {
            var response = new ServiceResponse<string>();


            var booleanFieldsNames = new List<BooleanFieldName>();
            var stringFieldsNames = new List<StringFieldName>();
            var multilineFieldsNames = new List<MultilineFieldName>();
            var dateTimeFieldsNames = new List<DateTimeFieldName>();
            var integerFieldsNames = new List<IntegerFieldName>();
            if(request.BooleanFieldsNames!=null)
            foreach (string fieldName in request.BooleanFieldsNames)
            {
            booleanFieldsNames.Add(new BooleanFieldName { Name = fieldName });
            }
            if (request.StringFieldsNames != null)
            foreach (string fieldName in request.StringFieldsNames)
            {
            stringFieldsNames.Add(new StringFieldName { Name = fieldName });
            }
            if(request.MultilineFieldsNames!=null)
            foreach (string fieldName in request.MultilineFieldsNames)
            {
            multilineFieldsNames.Add(new MultilineFieldName { Name = fieldName });
            }
            if(request.DateTimeFieldsNames!= null)
            foreach (string fieldName in request.DateTimeFieldsNames)
            {
            dateTimeFieldsNames.Add(new DateTimeFieldName { Name = fieldName });
            }
            if(request.IntegerFieldsNames != null)
            foreach (string fieldName in request.IntegerFieldsNames)
            {
            integerFieldsNames.Add(new IntegerFieldName { Name = fieldName });
            }

            var collection = new Models.Collection
            {
                Description = request.Description,
                Name = request.Name,
                ImageHandle = request.ImageHandle == string.Empty ? placeHolderHandle : request.ImageHandle,
                Topic = request.Topic,
                BooleanFieldsNames = booleanFieldsNames,
                DateTimeFieldsNames = dateTimeFieldsNames,
                IntegerFieldsNames = integerFieldsNames,
                MultilineFieldsNames = multilineFieldsNames,
                StringFieldsNames = stringFieldsNames,
                User = await context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId())
            };
            context.Collections.Add(collection);
            await context.SaveChangesAsync();


            response.Data = collection.Id.ToString();
            return response;
        }

        public async Task<ServiceResponse<List<CollectionGetDto>>> GetAllCollections()
        {
            var response = new ServiceResponse<List<CollectionGetDto>>();

            List<CollectionGetDto> collections = new List<CollectionGetDto>();

            var DbCollections = await context.Collections.ToListAsync();

            collections = DbCollections.Select(c => new CollectionGetDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Topic = c.Topic,
                ModifyTime = c.ModifyTime.ToString(),
                CreationTime = c.CreationTime.ToString(),
                ImageHandle = c.ImageHandle
            }).ToList();

            response.Data = collections;

            return response;

        }

        public async Task<ServiceResponse<CollectionGetDto>> GetCollectionById(int id)
        {
            var response = new ServiceResponse<CollectionGetDto>();

            var DbCollection = await context.Collections.FirstOrDefaultAsync(c => c.Id == id);

            response.Data = new CollectionGetDto
            {
                Id = DbCollection.Id,
                Name = DbCollection.Name,
                Description = DbCollection.Description,
                Topic = DbCollection.Topic,
                ModifyTime = DbCollection.ModifyTime.ToString(),
                CreationTime = DbCollection.CreationTime.ToString(),
                ImageHandle = DbCollection.ImageHandle
            };

            return response;
        }

        public async Task<ServiceResponse<List<CollectionGetDto>>> GetCollectionsByUserId(int id)
        {
            var response = new ServiceResponse<List<CollectionGetDto>>();

            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);

            var DbCollections = context.Collections.Where(c => c.User == user);

            var collections = await DbCollections.Select(c => new CollectionGetDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Topic = c.Topic,
                ModifyTime = c.ModifyTime.ToString(),
                CreationTime = c.CreationTime.ToString(),
                ImageHandle = c.ImageHandle
            }).ToListAsync();

            response.Data = collections;

            return response;

        }

        public async Task<ServiceResponse<bool>> CheckIfUserCanManageCollection(int id)
        {
            var response = new ServiceResponse<bool>();


            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            var collection = await context.Collections.FirstOrDefaultAsync(c => c.Id == id);
            response.Data = collection.User == user || user.isAdmin;


            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteCollection(int id)
        {
            var response = new ServiceResponse<bool>();

            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            var collection = await context.Collections.FirstOrDefaultAsync(c => c.Id == id);


            response.Success = collection.User == user || user.isAdmin;





            if (response.Success)
            {
                context.Collections.Remove(collection);
                await context.SaveChangesAsync();
            }
            return response;

        }

        public async Task<ServiceResponse<CollectionAddItemDto>> GetCollectionFields(int id)
        {
            var response = new ServiceResponse<CollectionAddItemDto>();
            var collection = await context.Collections.FirstOrDefaultAsync(c => c.Id == id);
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            if(collection==null)
            {
                response.Success = false;
                return response;
            }

            response.Success = collection.User == user || user.isAdmin;



            if (response.Success)
            {
                var booleanFieldsNames = new List<string>();
                foreach (BooleanFieldName fieldName in await context.BooleanFieldsNames.Where(f => f.Collection == collection).ToListAsync())
                {
                    booleanFieldsNames.Add(fieldName.Name);
                }
                var stringFieldsNames = new List<string>();
                foreach (StringFieldName fieldName in await context.StringFieldsNames.Where(f => f.Collection == collection).ToListAsync())
                {
                    stringFieldsNames.Add(fieldName.Name);
                }
                var multilineFieldsNames = new List<string>();
                foreach (MultilineFieldName fieldName in await context.MultilineFieldsNames.Where(f => f.Collection == collection).ToListAsync())
                {
                    multilineFieldsNames.Add(fieldName.Name);
                }
                var dateTimeFieldsNames = new List<string>();
                foreach (DateTimeFieldName fieldName in await context.DateTimeFieldsNames.Where(f => f.Collection == collection).ToListAsync())
                {
                    dateTimeFieldsNames.Add(fieldName.Name);
                }
                var integerFieldsNames = new List<string>();
                foreach (IntegerFieldName fieldName in await context.IntegerFieldsNames.Where(f => f.Collection == collection).ToListAsync())
                {
                    integerFieldsNames.Add(fieldName.Name);
                }
                response.Data = new CollectionAddItemDto
                {

                    Name = collection.Name,

                    Usersname = user.Name,

                    BooleanFieldsNames = booleanFieldsNames,
                    IntegerFieldsNames = integerFieldsNames,
                    DateFieldsNames = dateTimeFieldsNames,
                    MultilineFieldsNames = multilineFieldsNames,
                    StringFieldsNames = stringFieldsNames,

                };
            }




            return response;
        }

        public async Task<ServiceResponse<CollectionAddItemDto>> GetCollectionFieldsAnonymous(int id)
        {
            var response = new ServiceResponse<CollectionAddItemDto>();
            var collection = await context.Collections.FirstOrDefaultAsync(c => c.Id == id);
            var user = await context.Users.FirstOrDefaultAsync(u => u.Collections.Contains(collection));

            if (collection == null)
            {
                response.Success = false;
                return response;
            }




            var booleanFieldsNames = new List<string>();
            foreach (BooleanFieldName fieldName in await context.BooleanFieldsNames.Where(f => f.Collection == collection).ToListAsync())
            {
                booleanFieldsNames.Add(fieldName.Name);
            }
            var stringFieldsNames = new List<string>();
            foreach (StringFieldName fieldName in await context.StringFieldsNames.Where(f => f.Collection == collection).ToListAsync())
            {
                stringFieldsNames.Add(fieldName.Name);
            }
            var multilineFieldsNames = new List<string>();
            foreach (MultilineFieldName fieldName in await context.MultilineFieldsNames.Where(f => f.Collection == collection).ToListAsync())
            {
                multilineFieldsNames.Add(fieldName.Name);
            }
            var dateTimeFieldsNames = new List<string>();
            foreach (DateTimeFieldName fieldName in await context.DateTimeFieldsNames.Where(f => f.Collection == collection).ToListAsync())
            {
                dateTimeFieldsNames.Add(fieldName.Name);
            }
            var integerFieldsNames = new List<string>();
            foreach (IntegerFieldName fieldName in await context.IntegerFieldsNames.Where(f => f.Collection == collection).ToListAsync())
            {
                integerFieldsNames.Add(fieldName.Name);
            }

            response.Data = new CollectionAddItemDto
            {

                Name = collection.Name,

                Usersname = user.Name,

                    BooleanFieldsNames = booleanFieldsNames,
                    IntegerFieldsNames = integerFieldsNames,
                    DateFieldsNames = dateTimeFieldsNames,
                    MultilineFieldsNames = multilineFieldsNames,
                    StringFieldsNames = stringFieldsNames,

                };

            return response;
        }

        public async Task<ServiceResponse<int>> EditCollection(CollectionEditDto request, int id)
        {
            var response = new ServiceResponse<int>();
            var collection = await context.Collections.FirstOrDefaultAsync(c => c.Id == id);
            response.Data = collection.Id;
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            response.Success = collection.User == user || user.isAdmin;



            if(response.Success)
            {
                collection.Description = request.Description;
                collection.Name = request.Name;
                collection.Topic = request.Topic;
                if(request.ImageHandle==string.Empty)
                {
                    collection.ImageHandle = collection.ImageHandle;
                }
                else
                {
                    collection.ImageHandle = request.ImageHandle;
                }

                await context.SaveChangesAsync();
            }



            return response;
        }
    }
}
