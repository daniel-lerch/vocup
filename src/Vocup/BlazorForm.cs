using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace Vocup
{
    public partial class BlazorForm : Form
    {
        public BlazorForm()
        {
            InitializeComponent();

            var services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();
            services.AddSingleton(Close);
            blazorWebView1.HostPage = "wwwroot\\index.html";
            blazorWebView1.Services = services.BuildServiceProvider();
            blazorWebView1.RootComponents.Add<Razor.PracticeView>("#app");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using var form = new BlazorForm();
            form.ShowDialog();
        }
    }
}
