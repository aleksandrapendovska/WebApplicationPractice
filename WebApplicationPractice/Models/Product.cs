namespace WebApplicationPractice.Models
{
	public class Product
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public float Price { get; set; }

        public string GetFormattedPrice()
        {
            return string.Format("{0:0.00} ден", Price);
        }
    }
}
