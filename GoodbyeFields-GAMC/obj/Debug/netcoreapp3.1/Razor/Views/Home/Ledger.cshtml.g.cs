#pragma checksum "E:\GoodbyeFields-GAMC\Project\GoodbyeFields-GAMC\GoodbyeFields-GAMC\Views\Home\Ledger.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a56fb9bec939e8286360c4ad8a47c6926f9af453"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Ledger), @"mvc.1.0.view", @"/Views/Home/Ledger.cshtml")]
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
#line 1 "E:\GoodbyeFields-GAMC\Project\GoodbyeFields-GAMC\GoodbyeFields-GAMC\Views\_ViewImports.cshtml"
using GoodbyeFields_GAMC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\GoodbyeFields-GAMC\Project\GoodbyeFields-GAMC\GoodbyeFields-GAMC\Views\_ViewImports.cshtml"
using GoodbyeFields_GAMC.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a56fb9bec939e8286360c4ad8a47c6926f9af453", @"/Views/Home/Ledger.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"23fc625424e28ca2ab8ab618b30b28f2e67777ba", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Ledger : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<GoodbyeFields_GAMC_Model.Transaction>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\GoodbyeFields-GAMC\Project\GoodbyeFields-GAMC\GoodbyeFields-GAMC\Views\Home\Ledger.cshtml"
 if (Model != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div style=""width:1000px; height:500px; overflow:auto;"">
        <table style=""height:500px; overflow:scroll;"">
            <thead>
                <tr>
                    <td>TransactionId </td>
                    <td>TransactionTypeId </td>
                    <td>TransactionAmount </td>
                    <td>PlayerId </td>
                    <td>TransactionDescription </td>
                    <td>TransactionCode </td>
                    <td>TransactionStatus </td>
                    <td>TransactionTime </td>
                    <td>IsVoid </td>
                    <td>VoidDate </td>
                    <td>VoidDescription </td>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 23 "E:\GoodbyeFields-GAMC\Project\GoodbyeFields-GAMC\GoodbyeFields-GAMC\Views\Home\Ledger.cshtml"
                 foreach (var m in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>");
#nullable restore
#line 26 "E:\GoodbyeFields-GAMC\Project\GoodbyeFields-GAMC\GoodbyeFields-GAMC\Views\Home\Ledger.cshtml"
                       Write(m.TransactionId);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                        <td>");
#nullable restore
#line 27 "E:\GoodbyeFields-GAMC\Project\GoodbyeFields-GAMC\GoodbyeFields-GAMC\Views\Home\Ledger.cshtml"
                       Write(m.TransactionTypeId);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                        <td>");
#nullable restore
#line 28 "E:\GoodbyeFields-GAMC\Project\GoodbyeFields-GAMC\GoodbyeFields-GAMC\Views\Home\Ledger.cshtml"
                       Write(m.TransactionAmount);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                        <td>");
#nullable restore
#line 29 "E:\GoodbyeFields-GAMC\Project\GoodbyeFields-GAMC\GoodbyeFields-GAMC\Views\Home\Ledger.cshtml"
                       Write(m.PlayerId);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                        <td>");
#nullable restore
#line 30 "E:\GoodbyeFields-GAMC\Project\GoodbyeFields-GAMC\GoodbyeFields-GAMC\Views\Home\Ledger.cshtml"
                       Write(m.TransactionDescription);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                        <td>");
#nullable restore
#line 31 "E:\GoodbyeFields-GAMC\Project\GoodbyeFields-GAMC\GoodbyeFields-GAMC\Views\Home\Ledger.cshtml"
                       Write(m.TransactionCode);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                        <td>");
#nullable restore
#line 32 "E:\GoodbyeFields-GAMC\Project\GoodbyeFields-GAMC\GoodbyeFields-GAMC\Views\Home\Ledger.cshtml"
                       Write(m.TransactionStatus);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                        <td>");
#nullable restore
#line 33 "E:\GoodbyeFields-GAMC\Project\GoodbyeFields-GAMC\GoodbyeFields-GAMC\Views\Home\Ledger.cshtml"
                       Write(m.TransactionTime);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                        <td>");
#nullable restore
#line 34 "E:\GoodbyeFields-GAMC\Project\GoodbyeFields-GAMC\GoodbyeFields-GAMC\Views\Home\Ledger.cshtml"
                       Write(m.IsVoid);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                        <td>");
#nullable restore
#line 35 "E:\GoodbyeFields-GAMC\Project\GoodbyeFields-GAMC\GoodbyeFields-GAMC\Views\Home\Ledger.cshtml"
                       Write(m.VoidDate);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                        <td>");
#nullable restore
#line 36 "E:\GoodbyeFields-GAMC\Project\GoodbyeFields-GAMC\GoodbyeFields-GAMC\Views\Home\Ledger.cshtml"
                       Write(m.VoidDescription);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                    </tr>\r\n");
#nullable restore
#line 38 "E:\GoodbyeFields-GAMC\Project\GoodbyeFields-GAMC\GoodbyeFields-GAMC\Views\Home\Ledger.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n");
#nullable restore
#line 42 "E:\GoodbyeFields-GAMC\Project\GoodbyeFields-GAMC\GoodbyeFields-GAMC\Views\Home\Ledger.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<GoodbyeFields_GAMC_Model.Transaction>> Html { get; private set; }
    }
}
#pragma warning restore 1591
