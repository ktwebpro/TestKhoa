#pragma checksum "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fd4b0a1b38776e748611e1080e8ed75ac59d9609"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Manage_ListUser), @"mvc.1.0.view", @"/Views/Manage/ListUser.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\_ViewImports.cshtml"
using TestKhoa;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\_ViewImports.cshtml"
using TestKhoa.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd4b0a1b38776e748611e1080e8ed75ac59d9609", @"/Views/Manage/ListUser.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"253609647dc7860e70955d04dc61c447c512f355", @"/Views/_ViewImports.cshtml")]
    public class Views_Manage_ListUser : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("/Manage/Save"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("frmThemMoiAccount"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 5 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
  
    ViewData["Title"] = "Danh sách thành viên";
    var _ListUser = (List<TestKhoa.Models.AccountModel>)ViewBag.ListUser;
    var _ListRole = (List<Microsoft.AspNetCore.Identity.IdentityRole>)ViewBag.ListRole;
    var _User = await UserManager.GetUserAsync(User);
    var _UserId = _User.Id;
    var _CurrentRole = (await UserManager.GetRolesAsync(_User))[0];

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""row"">
    <div class=""col-lg-6"">
        <h4>Danh sách thành viên</h4>
    </div>
    <div class=""col-lg-6"">
        <div class=""text-right"">
            <a id=""cmdAdd"" data-toggle=""modal"" data-target=""#modal_form_horizontal"" class=""btn btn-primary"" href=""javascript:void(0)"">Tạo mới thành viên</a>
        </div>
    </div>
</div>
<hr />
<table id=""myTable"" class=""ui celled table"">
    <thead>
        <tr>
            <th></th>
            <th>Username</th>
            <th>Họ tên</th>
            <th>Địa chỉ</th>
            <th>Email</th>
            <th>Điện thoại</th>
            <th>Giới tính</th>
            <th>Trạng thái</th>
            <th>Avatar</th>
            <th>Role</th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 40 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
         foreach (var mThanhVien in _ListUser)
        {
            string _GioiTinh = "", _TrangThai = "", _Avatar = "";
            if (!string.IsNullOrEmpty(mThanhVien.Gender))
            {
                _GioiTinh = mThanhVien.Gender == "1" ? "Nữ" : "Nam";
            }
            if (!string.IsNullOrEmpty(mThanhVien.Status))
            {
                _TrangThai = mThanhVien.Status == "1" ? "Đã kích hoạt" : "Chưa kích hoạt";
            }
            if (!string.IsNullOrEmpty(mThanhVien.Avatar))
            {
                _Avatar = "<img src='/images/" + mThanhVien.Avatar + "' style=max-width:50px; />";
            }
            string _Role = (await UserManager.GetRolesAsync(mThanhVien))[0];
            if (
                (_CurrentRole == "SuperAdmin")
                || (_CurrentRole == "Admin" && _Role == "User")
                )
            { 

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td style=\"width:80px;\">\r\n");
#nullable restore
#line 63 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
                     if (_UserId != mThanhVien.Id)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <a href=\"#\" style=\"display:inline\" class=\"btn btn-sm cmdDeleteUser\" data-id=\"");
#nullable restore
#line 65 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
                                                                                                Write(mThanhVien.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""">
                            <svg xmlns=""http://www.w3.org/2000/svg"" width=""16"" height=""16"" fill=""currentColor"" class=""bi bi-x-square"" viewBox=""0 0 16 16"">
                                <path d=""M14 1a1 1 0 0 1 1 1v12a1 1 0 0 1-1 1H2a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1h12zM2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2z"" />
                                <path d=""M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708z"" />
                            </svg>
                        </a>
                        <a href=""#"" style=""display:inline""
                           data-id=""");
#nullable restore
#line 72 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
                               Write(mThanhVien.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-role=\"");
#nullable restore
#line 72 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
                                                          Write(_Role);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-username=\"");
#nullable restore
#line 72 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
                                                                                 Write(mThanhVien.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-address=\"");
#nullable restore
#line 72 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
                                                                                                                     Write(mThanhVien.Address);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"\r\n                           data-email=\"");
#nullable restore
#line 73 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
                                  Write(mThanhVien.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-fullname=\"");
#nullable restore
#line 73 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
                                                                    Write(mThanhVien.FullName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-phone=\"");
#nullable restore
#line 73 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
                                                                                                      Write(mThanhVien.PhoneNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"\r\n                           data-gender=\"");
#nullable restore
#line 74 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
                                   Write(mThanhVien.Gender);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-status=\"");
#nullable restore
#line 74 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
                                                                    Write(mThanhVien.Status);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-avatar=\"");
#nullable restore
#line 74 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
                                                                                                     Write(mThanhVien.Avatar);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"""
                           data-toggle=""modal"" data-target=""#modal_form_horizontal"" class=""btn btn-sm cmdEditUser"">
                            <svg xmlns=""http://www.w3.org/2000/svg"" width=""16"" height=""16"" fill=""currentColor"" class=""bi bi-pencil-square"" viewBox=""0 0 16 16"">
                                <path d=""M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"" />
                                <path fill-rule=""evenodd"" d=""M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"" />
                            </svg>
                        </a>
");
#nullable restore
#line 81 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </td>\r\n                <td>");
#nullable restore
#line 83 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
               Write(mThanhVien.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 84 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
               Write(mThanhVien.FullName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 85 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
               Write(mThanhVien.Address);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 86 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
               Write(mThanhVien.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 87 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
               Write(mThanhVien.PhoneNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 88 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
               Write(_GioiTinh);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 89 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
               Write(_TrangThai);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 90 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
               Write(Html.Raw(_Avatar));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 91 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
               Write(_Role);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n");
#nullable restore
#line 93 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
            }
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n<div id=\"modal_form_horizontal\" class=\"modal fade\" tabindex=\"-1\">\r\n    <div class=\"modal-dialog modal-lg\">\r\n        <div class=\"modal-content\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fd4b0a1b38776e748611e1080e8ed75ac59d960916601", async() => {
                WriteLiteral(@"
                <input type=""hidden"" name=""Type"" id=""Type"" value=""_Create"" />
                <input type=""hidden"" name=""Id"" id=""Id"" value=""0"" />
                <div class=""modal-body"">
                    <fieldset class=""mb-3"">
                        <legend class=""text-uppercase font-size-sm font-weight-bold"" id=""frmTitle"">Thêm mới thành viên</legend>
                        <hr />
                        <div class=""form-group row"">
                            <label class=""col-form-label col-sm-3"">Role</label>
                            <div class=""col-sm-9"">
                                <select class=""form-control select-search"" name=""Role"" id=""RoleId"">
");
#nullable restore
#line 111 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
                                     foreach (var mRole in _ListRole.Where(p=>
                                            _CurrentRole == "SuperAdmin" || (_CurrentRole == "Admin" && p.Name == "User")
                                        ))
                                    {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fd4b0a1b38776e748611e1080e8ed75ac59d960918089", async() => {
#nullable restore
#line 115 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
                                                               Write(mRole.Name);

#line default
#line hidden
#nullable disable
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 115 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
                                           WriteLiteral(mRole.Name);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 116 "D:\Fpt\TestKhoa\TestKhoa\Src\TestKhoa\Views\Manage\ListUser.cshtml"
                                    }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                                </select>
                            </div>
                        </div>

                        <div class=""form-group row"">
                            <label class=""col-form-label col-sm-3"">UserName</label>
                            <div class=""col-sm-9"">
                                <input type=""text"" required name=""UserName"" id=""UserName"" class=""form-control"">
                            </div>
                        </div>

                        <div class=""form-group row"">
                            <label class=""col-form-label col-sm-3"">Password</label>
                            <div class=""col-sm-9"">
                                <input type=""password"" required name=""Password"" id=""Password"" class=""form-control"">
                            </div>
                        </div>

                        <div class=""form-group row"">
                            <label class=""col-form-label col-sm-3"">Họ tên</label>
                      ");
                WriteLiteral(@"      <div class=""col-sm-9"">
                                <input type=""text"" name=""FullName"" id=""FullName"" class=""form-control"">
                            </div>
                        </div>

                        <div class=""form-group row"">
                            <label class=""col-form-label col-sm-3"">Địa chỉ</label>
                            <div class=""col-sm-9"">
                                <input type=""text"" name=""Address"" id=""Address"" class=""form-control"">
                            </div>
                        </div>

                        <div class=""form-group row"">
                            <label class=""col-form-label col-sm-3"">Email</label>
                            <div class=""col-sm-9"">
                                <input type=""email"" name=""Email"" id=""Email"" class=""form-control"">
                            </div>
                        </div>

                        <div class=""form-group row"">
                            <label class=""col-f");
                WriteLiteral(@"orm-label col-sm-3"">Điện thoại</label>
                            <div class=""col-sm-9"">
                                <input type=""number"" name=""Phone"" id=""Phone"" class=""form-control"">
                            </div>
                        </div>

                        <div class=""form-group row"">
                            <label class=""col-form-label col-sm-3"">Giới tính</label>
                            <div class=""col-sm-9"">
                                <select class=""form-control"" name=""Gender"" id=""Gender"">
                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fd4b0a1b38776e748611e1080e8ed75ac59d960922987", async() => {
                    WriteLiteral("Nam");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fd4b0a1b38776e748611e1080e8ed75ac59d960924244", async() => {
                    WriteLiteral("Nữ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                                </select>
                            </div>
                        </div>

                        <div class=""form-group row"">
                            <label class=""col-form-label col-sm-3"">Kích hoạt</label>
                            <div class=""col-sm-9"">
                                <select class=""form-control"" name=""Status"" id=""Status"">
                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fd4b0a1b38776e748611e1080e8ed75ac59d960925905", async() => {
                    WriteLiteral("Có");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fd4b0a1b38776e748611e1080e8ed75ac59d960927161", async() => {
                    WriteLiteral("Không");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                                </select>
                            </div>
                        </div>

                        <div class=""form-group row"">
                            <label class=""col-form-label col-sm-3"">Avatar</label>
                            <div class=""col-sm-9"">
                                <p>
                                    <input type=""file"" name=""UpAvatar""");
                BeginWriteAttribute("value", " value=\"", 9980, "\"", 9988, 0);
                EndWriteAttribute();
                WriteLiteral(@" />
                                </p>
                                <p>
                                    <img style=""display:none;"" id=""imgAvatar"" alt=""Alternate Text"" />
                                </p>
                            </div>
                        </div>

                    </fieldset>

                    <div class=""modal-footer"">
                        <button type=""button"" class=""btn btn-link"" data-dismiss=""modal"">Đóng lại</button>
                        <button type=""submit"" id=""cmdSave"" class=""btn btn-primary"">Thêm mới</button>
                    </div>

                </div>
            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script src=""//cdn.datatables.net/1.11.1/js/jquery.dataTables.min.js""></script>
    <script src=""//cdnjs.cloudflare.com/ajax/libs/semantic-ui/2.3.1/semantic.min.js""></script>
    <script>
        $(document).ready(function () {
            $('#myTable').DataTable();
        });
        $('#cmdAdd').click(function () {
            $('#frmTitle').text('Thêm mới thành viên');
            $('#Type').val('_Create');
            $('#Id').val('0');
            $('#cmdSave').text('Thêm mới');
            $('#frmThemMoiAccount').trigger('reset');
            $('#imgAvatar').hide();
            $('#UserName').removeAttr('readonly');
        });
        $('.cmdDeleteUser').click(function () {
            var _Confirm = confirm('Bạn có chắc chắn muốn xóa không?');
            if (_Confirm) {
                var _Id = $(this).attr('data-id');
                $.ajax({
                    type: 'POST',
                    url: '/Manage/Remove',
                    data: { strId: _Id },
         ");
                WriteLiteral(@"           success: function (data) {
                        document.location.reload();
                    },
                    error: function (data) {

                    },
                });
            }
        });
        $('.cmdEditUser').click(function () {
            $('#UserName').attr('readonly', 'readonly');

            var _UserName = $(this).attr('data-username');
            var _FullName = $(this).attr('data-fullname');
            var _Address = $(this).attr('data-address');
            var _Email = $(this).attr('data-email');
            var _Phone = $(this).attr('data-phone');
            var _Role = $(this).attr('data-role');
            var _Gender = $(this).attr('data-gender');
            var _Status = $(this).attr('data-status');
            var _Id = $(this).attr('data-id');
            var _Avatar = $(this).attr('data-avatar');

            if (_Avatar != '') {
                $('#imgAvatar').show();
                $('#imgAvatar').attr('src','/imag");
                WriteLiteral(@"es/' + _Avatar);
            } else {
                $('#imgAvatar').hide();
            }

            $('#UserName').val(_UserName);
            $('#FullName').val(_FullName);
            $('#Address').val(_Address);
            $('#Email').val(_Email);
            $('#Phone').val(_Phone);
            $('#RoleId').val(_Role);
            $('#Gender').val(_Gender);
            $('#Status').val(_Status);
            $('#Type').val('_Edit');
            $('#Id').val(_Id);
            $('#frmTitle').text('Cập nhật thành viên');
            $('#cmdSave').text('Cập nhật');
        });
    </script>

");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public RoleManager<IdentityRole> RoleManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<AccountModel> UserManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
