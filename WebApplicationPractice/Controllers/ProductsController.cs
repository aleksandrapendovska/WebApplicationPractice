using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationPractice.Data;
using WebApplicationPractice.Models;

namespace WebApplicationPractice.Controllers
{
	public class ProductsController : Controller
	{
        private readonly ApplicationDbContext dbContext;

        public ProductsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

		[HttpGet]
        public async Task<IActionResult> Index()
		{
			var products = await dbContext.Products.ToListAsync();
			return View(products);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Product viewModel)
		{
			var product = new Product
			{
				Name = viewModel.Name,
				Price = viewModel.Price
			};
			await dbContext.Products.AddAsync(product);
			await dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Products");
        }

		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			var product = await dbContext.Products.FindAsync(id);
			return View(product);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Product viewModel)
		{
			var product = await dbContext.Products.FindAsync(viewModel.Id);
			if(product is not null)
			{
				product.Name = viewModel.Name;
				product.Price = viewModel.Price;
				await dbContext.SaveChangesAsync();
			}
			return RedirectToAction("Index", "Products");
		}

		[HttpPost]
		public async Task<IActionResult> Delete(Guid id)
		{
			var product = await dbContext.Products.FindAsync(id);
			if(product is not null)
			{
				dbContext.Products.Remove(product);
				await dbContext.SaveChangesAsync();
			}
            return RedirectToAction("Index", "Products");
        }
	}
}
