using finalinternshipproject.Data;
using finalinternshipproject.Dtos.Item;
using finalinternshipproject.Models.Fields;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;

namespace finalinternshipproject.Services.Item
{
    public class ItemService : IItemService
    {
        private readonly DataContext context;
        private readonly IHttpContextAccessor httpContext;

        public ItemService(DataContext context, IHttpContextAccessor httpContext)
        {
            this.context = context;
            this.httpContext = httpContext;
        }
        private int GetUserId()
        {
            return int.Parse(httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
        public async Task<ServiceResponse<int>> PostItem(ItemPostDto request, int id)
        {
            var response = new ServiceResponse<int>();

            var collection = await context.Collections.FirstOrDefaultAsync(c => c.Id == id);
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            response.Success = collection.User== user || user.isAdmin;

            MapStringsToFields(request.BooleanFields, request.IntegerFields, request.StringFields, request.MultilineFields, request.DateFields, out List<BooleanField> bField, out List<StringField> sField, out List<IntegerField> iField, out List<MultilineField> mField, out List<DateTimeField> dField);

            if (response.Success)
            {

                var tags = new List<Models.Tag>();
                foreach (string tag in request.Tags)
                {
                    tags.Add(new Models.Tag { TagValue = tag });
                }

                var item = new Models.Item
                {
                    Name = request.Name,
                    Tags = tags,
                    Collection = collection,
                    Like = new List<Like>(),
                    BooleanFields = bField,
                    DateFields = dField,
                    IntegerFields = iField,
                    StringFields = sField,
                    MultilineFields = mField
                };
            context.Items.Add(item);
            await context.SaveChangesAsync();

            response.Data = item.Id;
            }


            return response;

        }

        public async Task<ServiceResponse<ItemGetDto>> GetItemById(int id)
        {
            var response = new ServiceResponse<ItemGetDto>();
            

            var item = await context.Items.FirstOrDefaultAsync(i => i.Id == id);
            if(item!=null)
            {
                var likes = await context.Likes.CountAsync(l => l.Item == item);

                var tags = new List<string>();
                var itemTags = await context.Tags.Where(t => t.Item.Id == item.Id).ToListAsync();
                if (itemTags!=null)
                {
                    foreach(Models.Tag tag in itemTags)
                    {
                        tags.Add(tag.TagValue);
                    }
                }
                var bFields = await context.BooleanField.Where(f => f.Item == item).ToListAsync();
                var sFields = await context.StringField.Where(f => f.Item == item).ToListAsync();
                var dFields = await context.DateTimeField.Where(f => f.Item == item).ToListAsync();
                var mFields = await context.MultilineField.Where(f => f.Item == item).ToListAsync();
                var iFields = await context.IntegerField.Where(f => f.Item == item).ToListAsync();






                MapFieldsToStrings(out List<string> bValue, out List<string> iValue, out List<string> sValue, out List<string> mValue, out List<string> dValue, bFields,sFields,iFields,mFields,dFields);
                response.Data = new ItemGetDto()
                {
                    Name = item.Name,
                    Tags = tags,
                    Likes = likes,
                    BooleanFields = bValue,
                    IntegerFields = iValue,
                    StringFields = sValue,
                    MultilineFields = mValue,
                    DateFields = dValue



                };
            }
            else
            {
                response.Success = false;
                return response;
            }


            return response;
        }

        public async Task<ServiceResponse<bool>> CanUserManageItem(int id)
        {
            var response = new ServiceResponse<bool>();

            var item = await context.Items.FirstOrDefaultAsync(i => i.Id == id);
            var collection = await context.Collections.FirstOrDefaultAsync(c => c.Items.Contains(item));
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            response.Data = user == collection.User || user.isAdmin;
            return response;
        }

        public async Task<ServiceResponse<int>> DeleteItem(int id)
        {
            var response = new ServiceResponse<int>();

            var item = await context.Items.FirstOrDefaultAsync(i => i.Id == id);
            var collection = await context.Collections.FirstOrDefaultAsync(c => c.Items.Contains(item));
            response.Data = collection.Id;
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            response.Success= user == collection.User || user.isAdmin;

            if(response.Success)
            {
            context.Remove(item);
            await context.SaveChangesAsync();
            }
            return response;
        }
        public async Task<ServiceResponse<int>> EditItem(ItemEditDto request, int id)
        {
            var response = new ServiceResponse<int>();


            var item = await context.Items.FirstOrDefaultAsync(i => i.Id == id);
            var collection = await context.Collections.FirstOrDefaultAsync(c => c.Items.Contains(item));
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            response.Success = collection.User == user || user.isAdmin;


            if (response.Success)
            {
            MapStringsToFields(request.BooleanFields, request.IntegerFields, request.StringFields, request.MultilineFields, request.DateFields, out List<BooleanField> bField, out List<StringField> sField, out List<IntegerField> iField, out List<MultilineField> mField, out List<DateTimeField> dField);

                var tags = new List<Models.Tag>();
                if(request.Tags!=null)
                {

                    foreach (string tag in request.Tags)
                    {
                        tags.Add(new Models.Tag { TagValue = tag });
                    }
                    context.Tags.RemoveRange(await context.Tags.Where(t => t.Item == item).ToListAsync());
                    item.Tags = tags;
                }
                else
                {
                    item.Tags = await context.Tags.Where(l => l.Item.Equals(item)).ToListAsync();
                }

                item.Name = request.Name;


                item.Like = await context.Likes.Where(l => l.Item.Equals(item)).ToListAsync();

                context.BooleanField.RemoveRange(context.BooleanField.Where(l => l.Item.Equals(item)));
                context.DateTimeField.RemoveRange(context.DateTimeField.Where(l => l.Item.Equals(item)));
                context.StringField.RemoveRange(context.StringField.Where(l => l.Item.Equals(item)));
                context.MultilineField.RemoveRange(context.MultilineField.Where(l => l.Item.Equals(item)));
                context.IntegerField.RemoveRange(context.IntegerField.Where(l => l.Item.Equals(item)));


                item.BooleanFields = bField;
                item.DateFields = dField;
                item.IntegerFields = iField;
                item.StringFields = sField;
                item.MultilineFields = mField;

                await context.SaveChangesAsync();

                response.Data = item.Id;
            }


            return response;
        }

        public void MapStringsToFields(List<string> bValue, List<string> iValue, List<string> sValue, List<string> mValue, List<string> dValue, out List<BooleanField> bField, out List<StringField> sField, out List<IntegerField> iField, out List<MultilineField> mField, out List<DateTimeField> dField)
        {
            bField = new List<BooleanField>();
            foreach(string value in bValue)
            {
                if (value.Equals("true"))
                { bField.Add(new BooleanField { Value = true }); }
                else
                { bField.Add(new BooleanField { Value = false }); }



            }
            iField = new List<IntegerField>();
            foreach (string value in iValue)
            {            
                iField.Add(new IntegerField { Value = int.Parse(value) });
            }
            dField = new List<DateTimeField>();
            foreach (string value in dValue)
            {
                DateTime newValue;
                string newString = value;
                newString = newString.Replace('T', ' ');
                newValue = DateTime.ParseExact(newString+":00Z","u",null);
                dField.Add(new DateTimeField { Value = newValue });
            }
            sField = new List<StringField>();
            foreach (string value in sValue)
            {
                sField.Add(new StringField { Value = value });
            }
            mField = new List<MultilineField>();
            foreach (string value in mValue)
            {
                mField.Add(new MultilineField { Value = value });
            }
        }


        public void MapFieldsToStrings (out List<string> bValue, out List<string> iValue, out List<string> sValue, out List<string> mValue, out List<string> dValue, List<BooleanField> bField, List<StringField> sField, List<IntegerField> iField, List<MultilineField> mField, List<DateTimeField> dField)
        {
            bValue = new List<string>();
            foreach(BooleanField boolField in bField)
            {
                if(boolField.Value)
                {
                    bValue.Add("on");
                }
                else
                {
                    bValue.Add("off");
                }
            }
            iValue = new List<string>();
            foreach (IntegerField intField in iField)
            {
                iValue.Add(intField.Value.ToString());
            }
            dValue = new List<string>();
            foreach (DateTimeField dateField in dField)
            {
                dValue.Add(String.Format("{0:u}", dateField.Value));
            }
            sValue = new List<string>();
            foreach (StringField stringField in sField)
            {
                sValue.Add(stringField.Value);
            }
            mValue = new List<string>();
            foreach (MultilineField multilineField in mField)
            {
                mValue.Add(multilineField.Value);
            }           
        }

        public async Task<ServiceResponse<List<ItemLatestDto>>> GetLatest(int ammount)
        {
            var response = new ServiceResponse<List<ItemLatestDto>>();

            var items =await context.Items.OrderByDescending(i => i.Id).Take(ammount).ToListAsync();


            var itemsList = new List<ItemLatestDto>();
            foreach (Models.Item item in items)
            {
                var coll = await context.Collections.FirstOrDefaultAsync(c => c.Items.Contains(item));

                var author = await context.Users.FirstOrDefaultAsync(u => u.Collections.Contains(coll));

                itemsList.Add(new ItemLatestDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    UsersName = author.Name,
                    CollectionsName = coll.Name,
                    CollectionId = coll.Id


                }) ;
            }


            response.Data = itemsList;

            return response;
        }

        public async Task<ServiceResponse<List<ItemGetDto>>> GetItemByCollectionId(int id)
        {
            var response = new ServiceResponse<List<ItemGetDto>>();

            var collection = await context.Collections.FirstOrDefaultAsync(c => c.Id == id);

            var items = await context.Items.Where(i => i.Collection == collection).ToListAsync();

            var responseItems = new List<ItemGetDto>();


            
            if (items != null)
            {
                foreach (Models.Item item in items)
                {

                    var likes = await context.Likes.CountAsync(l => l.Item == item);

                    var tags = new List<string>();
                    var itemTags = await context.Tags.Where(t => t.Item.Id == item.Id).ToListAsync();
                    if (itemTags != null)
                    {
                        foreach (Models.Tag tag in itemTags)
                        {
                            tags.Add(tag.TagValue);
                        }
                    }
                    var bFields = await context.BooleanField.Where(f => f.Item == item).ToListAsync();
                    var sFields = await context.StringField.Where(f => f.Item == item).ToListAsync();
                    var dFields = await context.DateTimeField.Where(f => f.Item == item).ToListAsync();
                    var mFields = await context.MultilineField.Where(f => f.Item == item).ToListAsync();
                    var iFields = await context.IntegerField.Where(f => f.Item == item).ToListAsync();






                    MapFieldsToStrings(out List<string> bValue, out List<string> iValue, out List<string> sValue, out List<string> mValue, out List<string> dValue, bFields, sFields, iFields, mFields, dFields);
                    responseItems.Add(new ItemGetDto()
                    {
                        Name = item.Name,
                        Tags = tags,
                        Likes = likes,
                        BooleanFields = bValue,
                        IntegerFields = iValue,
                        StringFields = sValue,
                        MultilineFields = mValue,
                        DateFields = dValue



                    });
                }
                response.Data = responseItems; 
            }
            else
            {
                response.Success = false;
                return response;
            }

            
            return response;
        }

        public Task<ServiceResponse<List<ItemGetDto>>> SearchForItem(string searchQuery)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<int>>> CollectionItems(int id)
        {
            var response = new ServiceResponse<List<int>>();
            var ints = new List<int>();

            var collection = await context.Collections.FirstOrDefaultAsync(c => c.Id == id);

            var items = await context.Items.Where(i => i.Collection.Equals(collection)).ToListAsync();

            if(items!=null)
            {
                foreach(Models.Item item in items)
                {
                    ints.Add(item.Id);
                }
            }

            response.Data = ints;

            return response;
        }
    }
}
