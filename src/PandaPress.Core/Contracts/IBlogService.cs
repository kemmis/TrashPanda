﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PandaPress.Core.Models.Data;
using PandaPress.Core.Models.Request;
using PandaPress.Core.Models.View;

namespace PandaPress.Core.Contracts
{
    public interface IBlogService
    {
        PostViewModel GetPostBySlug(string slug);
        PostListViewModel GetPostList(PostListRequest request);

        void DeletePost(string blogId, string postId);
        void EditPost(PostEditRequest request);
        IEnumerable<PostEditRequest> GetCategories(string blogId, string userName);
        PostViewModel GetPost(string postId);
        IEnumerable<PostViewModel> GetRecentPosts(string blogId);
        void NewCategory(string blogId, string category);
        void NewMediaObject(string blogId);
        Post NewPost(PostCreateRequest request);
        List<Blog> GetBlogsForUser(string username);
        List<Category> GetCategories();
        Task<string> SaveMedia(byte[] bytes, string name);
        SettingsViewModel GetBlogSettings();
        SettingsViewModel SaveBlogSettings(SettingsViewModel settings);
        DashboardDataViewModel GetDashboardData();
        ContentViewModel GetContent();
        CategoryContentViewModel AddCategory(string title, string description);
        void DeleteCategory(int categoryId);
        ProfileSettingsViewModel GetProfileSettings(string userId);
        ProfileSettingsViewModel UpdateProfileSettings(string userId, ProfileSettingsUpdateRequest request);
        CommentViewModel SaveComment(CommentCreateRequest request);
        HomeViewModel GetHomeData();
        void DeletePost(int postId);
        void UnDeletePost(int postId);
        PostListViewModel GetPostCategoryList(PostListRequest request);
        List<CategoryContentViewModel> GetAllCategories();
        EditPostViewModel GetPostToEdit(int postId);
        EditPostViewModel SavePost(EditPostViewModel post, string username);
        Task<ProfileSettingsViewModel> SaveProfilePicture(string userId, IFormFile file);
        Task<MediaViewModel> UploadMedia(IFormFile file);
        ProfileSettingsViewModel RemoveProfilePhoto(string userId);
    }
}