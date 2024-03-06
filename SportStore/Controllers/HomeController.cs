using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Models.ViewModels;
using System.ComponentModel;
using System.Diagnostics;

namespace SportStore.Controllers
{
    // Контроллер для обработки запросов связанных с домашней страницей и ошибками
    public class HomeController : Controller
    {
        // Приватное поле для хранения экземпляра репозитория
        private IStoreRepositiry repositiry;

        // Конструктор для внедрения зависимости репозитория
        public HomeController(IStoreRepositiry repositiry)
        {
            this.repositiry = repositiry;
        }

        // Константа для указания размера страницы в пагинации
        public int PageSize = 4;

        // Метод действия для домашней страницы, отображающий список продуктов
        public ViewResult Index(string category, int productPage = 1)
        {
            // Возвращаем представление с моделью ProductsListViewModel, содержащую необходимые данные для представления
            return View(new ProductsListViewModel
            {
                // Запрос продуктов из репозитория в зависимости от указанной категории и параметров пагинации
                Products = repositiry.Products
                    .Where(p => category == null || p.Category == category) // Фильтрация по категории, если она указана
                    .OrderBy(p => p.ProductID) // Упорядочивание продуктов по идентификатору продукта
                    .Skip((productPage - 1) * PageSize) // Пропуск записей для пагинации
                    .Take(PageSize), // Взятие определенного количества записей на страницу
                // Предоставление информации о пагинации
                PaginInfo = new PaginInfo
                {
                    CurrentPage = productPage, // Номер текущей страницы
                    ItemsPerPage = PageSize, // Количество элементов на странице
                    TotalItems = repositiry.Products.Count() // Общее количество элементов
                }
            });
        }

        // Метод действия для отображения страницы с политикой конфиденциальности
        public IActionResult Privacy()
        {
            return View();
        }

        // Метод действия для обработки ошибок
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Возвращаем представление с деталями ошибки
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

