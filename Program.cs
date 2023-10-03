// See https://aka.ms/new-console-template for more information

ECommerce objECommerce = new ECommerce();

List<int> a = new List<int>();

a.Add(1);
//a.Add(1);
//a.Add(2);
//a.Add(2);
//a.Add(5);

int result = 0;

if (a.Count >= 1 && a.Count < 100)
{

    for (int i = 0; i < a.Count; i++)
    {
        int count = 0;
        for (int j = 0; j < a.Count; j++)
        {
            if (a[i] == a[j])
            {
                count++;
            }
        }
        if (count == 1)
        {
            result = a[i];
        }
        //break;
    }
}
return result;
    

//objECommerce.MainMethod();




public class ECommerce
{
    public static int FindLowestPrice(string[][] products, string[][] discounts)
    {
        Dictionary<string, List<(int type, double value)>> discountMap = new Dictionary<string, List<(int, double)>>();

        // Build a dictionary to map tags to their discounts
        foreach (string[] discount in discounts)
        {
            string tag = discount[0];
            int type = int.Parse(discount[1]);
            double value = double.Parse(discount[2]) / (type == 1 ? 100.0 : 1.0); // Convert percentage to fraction

            if (!discountMap.ContainsKey(tag))
            {
                discountMap[tag] = new List<(int, double)>();
            }

            discountMap[tag].Add((type, value));
        }

        int totalCost = 0;

        // Iterate through products
        foreach (string[] product in products)
        {
            int price = int.Parse(product[0]);
            double minDiscountedPrice = double.MaxValue;

            // Iterate through product tags
            for (int i = 1; i < product.Length; i++)
            {
                string tag = product[i];
                if (tag != "EMPTY" && discountMap.ContainsKey(tag))
                {
                    foreach ((int type, double value) in discountMap[tag])
                    {
                        double discountedPrice = type == 0 ? price - value : (type == 1 ? price * (1.0 - value) : price - value);
                        minDiscountedPrice = Math.Min(minDiscountedPrice, discountedPrice);
                    }
                }
            }

            totalCost += (int)Math.Round(minDiscountedPrice);
        }

        return totalCost;
    }

    public void MainMethod()
    {
        // Example usage
        string[][] products = new string[][] {
            new string[] { "10", "do", "d1" },
            new string[] { "15", "EMPTY", "EMPTY" },
            new string[] { "20", "d1", "EMPTY" }
        };

        string[][] discounts = new string[][] {
            new string[] { "do", "1", "27" },
            new string[] { "d1", "2", "5" }
        };

        int result = FindLowestPrice(products, discounts);
        Console.WriteLine("Total Cost: " + result); // Output: Total Cost: 35
    }
}

