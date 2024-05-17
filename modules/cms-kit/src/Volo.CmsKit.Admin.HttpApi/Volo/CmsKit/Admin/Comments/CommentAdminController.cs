﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;
using Volo.CmsKit.Features;
using Volo.CmsKit.GlobalFeatures;
using Volo.CmsKit.Permissions;

namespace Volo.CmsKit.Admin.Comments;

[RequiresFeature(CmsKitFeatures.CommentEnable)]
[Authorize(CmsKitAdminPermissions.Comments.Default)]
[RequiresGlobalFeature(typeof(CommentsFeature))]
[RemoteService(Name = CmsKitAdminRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitAdminRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit-admin/comments")]
public class CommentAdminController : CmsKitAdminController, ICommentAdminAppService
{
    protected ICommentAdminAppService CommentAdminAppService { get; }


    public CommentAdminController(ICommentAdminAppService commentAdminAppService)
    {
        CommentAdminAppService = commentAdminAppService;
    }

    [HttpGet]
    public virtual Task<PagedResultDto<CommentWithAuthorDto>> GetListAsync(CommentGetListInput input)
    {
        return CommentAdminAppService.GetListAsync(input);
    }

    [HttpGet]
    [Route("{id}")]
    public virtual Task<CommentWithAuthorDto> GetAsync(Guid id)
    {
        return CommentAdminAppService.GetAsync(id);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(CmsKitAdminPermissions.Comments.Delete)]
    public virtual Task DeleteAsync(Guid id)
    {
        return CommentAdminAppService.DeleteAsync(id);
    }

	[HttpPost]
	[Route("{id}")]
	public Task UpdateApprovalStatusAsync(Guid id, CommentApprovalDto commentApprovalDto)
    {
		return CommentAdminAppService.UpdateApprovalStatusAsync(id, commentApprovalDto);

	}
    [HttpPost]
    [Route("settings")]
    public Task SetSettings(SettingsDto settingsDto)
    {
       return CommentAdminAppService.SetSettings(settingsDto);
    }

    [HttpGet]
    [Route("settings")]

    public Task<SettingsDto> GetSettings()
    {
       return CommentAdminAppService.GetSettings();
    }
	[HttpGet]
	[Route("pending-count")]
	public Task<int> GetPendingCommentCount()
	{
		return CommentAdminAppService.GetPendingCommentCount();
	}
}
