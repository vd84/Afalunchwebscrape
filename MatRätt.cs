namespace maträtter {
    public class MatRätt {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Pris { get; set; }

        public decimal CalcaulateDiscountedPricePercent(int percent){
            return (this.Pris * (100 - percent))/100;
        }

    }
}