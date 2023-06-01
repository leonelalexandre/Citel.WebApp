using Citel.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Citel.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly string ENDPOINT = "http://localhost:21456/api/Category";
        private readonly HttpClient httpClient = null;

        public CategoryController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ENDPOINT);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                CategoryViewModel viewModel = null;

                string url = $"{ENDPOINT}/GetById/{id}";
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    viewModel = JsonConvert.DeserializeObject<CategoryViewModel>(content);
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CategoryViewModel category)
        {
            try
            {
                string json = JsonConvert.SerializeObject(category);
                byte[] buffer = Encoding.UTF8.GetBytes(json);

                ByteArrayContent content = new ByteArrayContent(buffer);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                string url = $"{ENDPOINT}/Add";
                var reponse = await httpClient.PostAsync(url, content);

                if (!reponse.IsSuccessStatusCode)
                    ModelState.AddModelError(null, "Erro ao salvar a categoria");

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Put([FromForm] CategoryViewModel category)
        {
            try
            {
                string json = JsonConvert.SerializeObject(category);
                byte[] buffer = Encoding.UTF8.GetBytes(json);

                ByteArrayContent content = new ByteArrayContent(buffer);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                string url = $"{ENDPOINT}/Edit";
                var reponse = await httpClient.PutAsync(url, content);

                if (!reponse.IsSuccessStatusCode)
                    ModelState.AddModelError(null, "Erro ao salvar a categoria");

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                List<CategoryViewModel> categories = null;
                string url = $"{ENDPOINT}/SelecionarTodos";
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(content);
                }
                else
                    ModelState.AddModelError(null, "Houve um erro ao buscar as Categorias!");


                return View(categories);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IActionResult> ViewCategory(int id)
        {
            try
            {
                CategoryViewModel viewModel = null;

                string url = $"{ENDPOINT}/GetById/{id}";
                var response = await httpClient.GetAsync(url);

                if(response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    viewModel = JsonConvert.DeserializeObject<CategoryViewModel>(content);
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                CategoryViewModel viewModel = null;

                string url = $"{ENDPOINT}/GetById/{id}";
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    viewModel = JsonConvert.DeserializeObject<CategoryViewModel>(content);
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                string url = $"{ENDPOINT}/Delete/{id}";
                var response = await httpClient.DeleteAsync(url);

                if (!response.IsSuccessStatusCode)
                    ModelState.AddModelError(null, "Erro ao salvar a categoria");

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
