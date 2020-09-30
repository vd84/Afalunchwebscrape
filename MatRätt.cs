namespace maträtter {
    public class MatRätt {
        public int Id { get; set; }
        public string Title { get; set; }
        public string NameOfRestaurant { get; set; }
        public string Ingredients { get; set; }
        public int Price { get; set; }
        public decimal CalcaulateDiscountedPricePercent(int percent){
            return (this.Price * (100 - percent))/100;
        }
    }
}