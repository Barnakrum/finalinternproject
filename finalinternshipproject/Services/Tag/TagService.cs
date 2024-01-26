using finalinternshipproject.Data;
using finalinternshipproject.Dtos.Tag;
using finalinternshipproject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace finalinternshipproject.Services.Tag
{
    public class TagService : ITagService
    {
        private readonly DataContext context;

        public TagService(DataContext context)
        {
            this.context = context;
        }
        public async Task<ServiceResponse<List<string>>> GetAllTags()
        {
            var response = new ServiceResponse<List<string>>();
            var tags = new List<string>();

            var dbTags = await context.Tags.ToListAsync();

            foreach (Models.Tag tag in dbTags)
            {
                if(!tags.Contains(tag.TagValue))
                {
                    tags.Add(tag.TagValue);
                }
            }
            response.Data = tags;

            return response;
        }

        public async Task<ServiceResponse<List<TagForCloudDto>>> GetTagsForCloud()
        {
            var response = new ServiceResponse<List<TagForCloudDto>>();


            var dbTags = await context.Tags.ToListAsync();

            var tagsValues = new List<String>();
            foreach (var tag in dbTags)
            {
                tagsValues.Add(tag.TagValue);
            }

            var groupedTagsValues = tagsValues.GroupBy(i => i);

            var tagsList = new List<TagForCloudDto>();
            foreach(var tag in groupedTagsValues)
            {
                
                tagsList.Add(new TagForCloudDto {Value = tag.Key, Count = tag.Count()});
            }
            response.Data = tagsList;

            return response;
        }
    }
}
