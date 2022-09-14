using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BikesShopWebApiClient
{
    public partial class Form1 : Form
    {
        public HttpClient client = new HttpClient();
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            List<Bike> bikes = new List<Bike>();
            bikes = await GetBikesAsync();
            foreach(var a in bikes)
            {
                listBox1.Items.Add($"Title - {a.Title}; Id - {a.BikeId}");
                listBox2.Items.Add($"Title - {a.Title}; Id - {a.BikeId}");
            }
        }

        public async Task<List<Bike>> GetBikesAsync()
        {
            List<Bike> bikes = new List<Bike>();
            HttpResponseMessage response = await client.GetAsync("https://localhost:44316/api/bikes");
            if (response.IsSuccessStatusCode)
            {
                bikes = await response.Content.ReadAsAsync<List<Bike>>();
            }
            return bikes;
        }

        public async Task DeleteBikeAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"https://localhost:44316/api/bikes/{id}");

        }

        public async Task UpdateBikeAsync(int id)
        {
            Bike bike = new Bike()
            {
                BikeId = id,
                Title = textBox12.Text,
                Price = double.Parse(textBox11.Text),
                WheelSize = float.Parse(textBox10.Text),
                Info = textBox9.Text,
                Color = textBox8.Text,
                Material = textBox7.Text

            };

            HttpResponseMessage response = await client.PutAsJsonAsync($"https://localhost:44316/api/bikes", bike);
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateBikeAsync()
        {
            Bike bike = new Bike()
            {
                
                Title = textBox1.Text,
                Price = double.Parse(textBox2.Text),
                WheelSize = float.Parse(textBox3.Text),
                Info = textBox4.Text,
                Color = textBox5.Text,
                Material = textBox6.Text

            };

            HttpResponseMessage response = await client.PostAsJsonAsync($"https://localhost:44316/api/bikes", bike);
            response.EnsureSuccessStatusCode();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            int id = ((int)numericUpDown2.Value);
            await UpdateBikeAsync(id);

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await CreateBikeAsync();
        }

        private async Task UpdateLists()
        {
            List<Bike> bikes = new List<Bike>();
            bikes = await GetBikesAsync();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox1.Update();
            listBox2.Update();
            foreach (var a in bikes)
            {
                listBox1.Items.Add($"Title - {a.Title}; Id - {a.BikeId}");
                listBox2.Items.Add($"Title - {a.Title}; Id - {a.BikeId}");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int id = ((int)numericUpDown1.Value);
            await DeleteBikeAsync(id);
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await UpdateLists();
        }
    }
}
