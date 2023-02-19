using ClickSharp.Configuration;
using System.Text;

namespace ClickSharp.Pages.Admin
{
    public partial class EditPage
    {
        private Dictionary<string, object>? editorConf;

        private void LoadEditor()
        {
            List<Dictionary<string, object>> images = new List<Dictionary<string, object>>();
            var baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"Images");
            if (Directory.Exists(baseDirectory))
            {
                var tempImg = Directory.GetFiles(baseDirectory, "*.*");
                foreach (var img in tempImg)
                {
                    var path = $"{AppUrls.AppImages}/{img.Substring(baseDirectory.Length + 1)}";
                    images.Add(
                        new Dictionary<string, object>
                             {
                                {"title", path},
                                {"value", path}
                             }
                    );
                }
            }

            editorConf = new Dictionary<string, object>
        {
            {"toolbar",
                new StringBuilder()
                .Append("undo redo| epi-link | epi-image-editor | epi-personalized-content | cut copy paste | ")
                .Append("styleselect formatselect | bold italic underline strikethrough | forecolor backcolor removeformat | ")
                .Append("alignleft aligncenter alignright alignjustify | numlist bullist outdent indent | table | toc | codesample code template")
                .Append(" | styleselect | fontselect | fontsizeselect | ")
                .Append("link image media | emoticons | preview print | fullscreen")
                .ToString()
            },
            {"plugins",
                new StringBuilder()
                .Append("epi-link epi-image-editor epi-dnd-processor epi-personalized-content autolink ")
                .Append("visualblocks codesample toc ")
                .Append("insertdatetime imagetools colorpicker textpattern help" )
                .Append("save table contextmenu directionality emoticons paste textcolor ")
                .Append("searchreplace wordcount visualchars fullscreen media nonbreaking ")
                .Append("advlist link image lists charmap print preview hr anchor pagebreak spellchecker ")
                .Append("template code")
                .ToString()
            },
            {"style_formats", new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    {"title", "Bold text"},
                    {"inline", "b"}
                },        new Dictionary<string, object>
                {
                    {"title", "Red text"},
                    {"inline", "span"},
                    {"styles", new Dictionary<string,string> { {"color", "#ff0000"} } }
                },
                        new Dictionary<string, object>
                {
                    {"title", "Red header"},
                    {"inline", "h1"},
                    {"styles", new Dictionary<string,string> { {"color", "#ff0000"} } }
                },
                        new Dictionary<string, object>
                {
                    {"title", "Example 1"},
                    {"inline", "span"},
                    {"classes", "example1"}
                },
                        new Dictionary<string, object>
                {
                    {"title", "Example 2"},
                    {"inline", "span"},
                    {"classes", "example2"}
                }
            } },
            {"extended_valid_elements", "emstart[*],emend[*]"},
            {"templates", new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    {"title", "sample"},
                    {"description", "description"},
                    {"content", "<div style=\"color:red\">element<p style=\"color:blue\">test</p></div>"}
                },
                        new Dictionary<string, object>
                {
                    {"title", "sample2"},
                    {"description", "description2"},
                    {"content", "<div style=\"color:blue\">element<p style=\"color:red\">test</p></div>"}
                }
            }
            },
            {"paste_word_valid_elements", "b,strong,i,em,h1,h2,table[border|width|style|height|align|valign],tr,td[colspan|rowspan|width|style|height|align|valign],tbody,th,p" },
            {"font_size_style_values", "10pt,12pt,14pt,18pt,24pt,36pt,48pt"},
            {"image_list", images},
            {"promotion", false },
            {"branding", false }
        };
        }
    }
}
