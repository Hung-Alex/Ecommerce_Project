using Domain.Entities;
using Domain.Entities.Banners;
using Domain.Entities.Brands;
using Domain.Entities.Category;
using Domain.Entities.Products;
using Domain.Entities.Slides;

namespace Infrastructure.Data.Seed
{
    public class SeedData : ISeed
    {
        private readonly string[] ImageProduct =
        {
            "https://images.pexels.com/photos/1313267/pexels-photo-1313267.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/46174/strawberries-berries-fruit-freshness-46174.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/1132047/pexels-photo-1132047.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/1395958/pexels-photo-1395958.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/1435735/pexels-photo-1435735.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/709567/pexels-photo-709567.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/102104/pexels-photo-102104.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/557659/pexels-photo-557659.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/109274/pexels-photo-109274.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/197907/pexels-photo-197907.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/1028598/pexels-photo-1028598.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/616838/pexels-photo-616838.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/867349/pexels-photo-867349.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/461415/pexels-photo-461415.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/327098/pexels-photo-327098.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/1414130/pexels-photo-1414130.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/60021/grapes-wine-fruit-vines-60021.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/1437598/pexels-photo-1437598.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/2161935/pexels-photo-2161935.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/1071878/pexels-photo-1071878.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/992819/pexels-photo-992819.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/1374651/pexels-photo-1374651.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/892808/pexels-photo-892808.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/259763/pexels-photo-259763.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/2539177/pexels-photo-2539177.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/54332/currant-immature-bush-berry-54332.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/1367242/pexels-photo-1367242.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/139229/pexels-photo-139229.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/1587839/pexels-photo-1587839.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/1071882/pexels-photo-1071882.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/952369/pexels-photo-952369.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/1424457/pexels-photo-1424457.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/314834/pexels-photo-314834.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/1843385/pexels-photo-1843385.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/173952/pexels-photo-173952.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/1153655/pexels-photo-1153655.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/137119/pexels-photo-137119.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/814533/pexels-photo-814533.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/989200/pexels-photo-989200.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/693794/pexels-photo-693794.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/1116558/pexels-photo-1116558.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/18509304/pexels-photo-18509304/free-photo-of-dia-rau-xa-lach-rau-b-a-an.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/1656664/pexels-photo-1656664.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/3487715/pexels-photo-3487715.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/1556665/pexels-photo-1556665.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/760281/pexels-photo-760281.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/1300975/pexels-photo-1300975.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/728393/pexels-photo-728393.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/1425358/pexels-photo-1425358.jpeg?auto=compress&cs=tinysrgb&w=600",
            "https://images.pexels.com/photos/94442/pexels-photo-94442.jpeg?auto=compress&cs=tinysrgb&w=600",
        };
        private readonly string[] ImageSlides =
            {
                "https://images.pexels.com/photos/257816/pexels-photo-257816.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                "https://images.pexels.com/photos/319798/pexels-photo-319798.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                "https://images.pexels.com/photos/2252584/pexels-photo-2252584.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                "https://images.pexels.com/photos/23228994/pexels-photo-23228994/free-photo-of-g-thien-nhien-hoa-mua-he.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                "https://images.pexels.com/photos/1458694/pexels-photo-1458694.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
            };
        private readonly StoreDbContext _dbContext;
        public SeedData(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task InitData()
        {
            _dbContext.Database.EnsureCreated();
            if (_dbContext.Categories.Any())
            {
                return;
            }
            var brandsData = BrandsInit();
            await _dbContext.AddRangeAsync(brandsData);
            var bannersData = BannerInit();
            await _dbContext.AddRangeAsync(bannersData);
            var parrentCategory = CategoryParrents();
            await _dbContext.AddRangeAsync(parrentCategory);
            var product = ProductInit(parrentCategory, brandsData);
            await _dbContext.AddRangeAsync(product);
            foreach (var item in product)
            {
                var images = AssignedImageForProduct(item);
                var variants= AssignedVariantForProduct(item);
                await _dbContext.AddRangeAsync(images);
                await _dbContext.AddRangeAsync(variants);
            }
            var slidesData = SlideInit();
            await _dbContext.AddRangeAsync(slidesData);
            foreach (var item in slidesData)
            {
                var images = AssignedImageForSlide(item);
                await _dbContext.AddRangeAsync(images);
            }
            await _dbContext.SaveChangesAsync();
        }
        private List<Slide> SlideInit()
        {
            List<Slide> slides = new List<Slide>()
            {
                new()
                {
                    Title="Home Page",
                    Description="Lorem ipsum dolor sit amet" +
                    ", consectetur adipiscing elit," +
                    " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." +
                    " Cras tincidunt lobortis feugiat vivamus at augue eget." +
                    " Posuere morbi leo urna molestie at. Accumsan tortor posuere ac ut consequat semper viverra nam." +
                    " Faucibus vitae aliquet nec ullamcorper sit amet risus nullam. Pellentesque elit ullamcorper dignissim cras",
                    IsActive=true,

                },
                new()
                {
                    Title="Home Page",
                    Description="Lorem ipsum dolor sit amet" +
                    ", consectetur adipiscing elit," +
                    " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." +
                    " Cras tincidunt lobortis feugiat vivamus at augue eget." +
                    " Posuere morbi leo urna molestie at. Accumsan tortor posuere ac ut consequat semper viverra nam." +
                    " Faucibus vitae aliquet nec ullamcorper sit amet risus nullam. Pellentesque elit ullamcorper dignissim cras",
                    IsActive=false,
                },
            };
            return slides;
        }
        private List<ProductSkus> AssignedVariantForProduct(Product product)
        {
            List<ProductSkus> images = new List<ProductSkus>()
            {
                new()
                {
                    ProductId=product.Id,
                    Name="5 KG",
                    Description="Oganic"
                },
                new()
                {
                    ProductId=product.Id,
                    Name="4 KG",
                    Description="Sea Food"
                },
                new()
                {
                   ProductId=product.Id,
                    Name="3 KG",
                    Description="Craw Fish"
                }
            };
            return images;
        }
        private List<Image> AssignedImageForSlide(Slide slide)
        {
            List<Image> images = new List<Image>()
            {
                new()
                {
                    ImageExtension="jpg",
                    ImageUrl=this.ImageSlides[0],
                    SlideId=slide.Id,
                    OrderItem=1,
                    PublicId="test"
                },
                new()
                {
                    ImageExtension="jpg",
                    ImageUrl=this.ImageSlides[1],
                    SlideId=slide.Id,
                    OrderItem=2,
                    PublicId="test"
                },
                new()
                {
                    ImageExtension="jpg",
                    ImageUrl=this.ImageSlides[2],
                    SlideId=slide.Id,
                    OrderItem=3,
                    PublicId="test"
                },
                new()
                {
                    ImageExtension="jpg",
                    ImageUrl=this.ImageSlides[3],
                    SlideId=slide.Id,
                    OrderItem=4,
                    PublicId="test"
                },
                new()
                {
                    ImageExtension="jpg",
                    ImageUrl=this.ImageSlides[4],
                    SlideId=slide.Id,
                    OrderItem=5,
                    PublicId="test"
                },
            };
            return images;
        }
        private List<Image> AssignedImageForProduct(Product product)
        {
            int arrayLength = this.ImageProduct.Length;
            int[] randomNumbers = GenerateUniqueRandomNumbers(arrayLength, 4);
            List<Image> imageProducts = new List<Image>()
            {
                new()
                {
                    ImageExtension="jpg",
                    ImageUrl=this.ImageProduct[randomNumbers[0]],
                    ProductId=product.Id,
                    OrderItem=1,
                    PublicId="test"
                },
                new()
                {
                    ImageExtension="jpg",
                    ImageUrl=this.ImageProduct[randomNumbers[1]],
                    ProductId=product.Id,
                    OrderItem=2,
                    PublicId="test"
                },
                new()
                {
                    ImageExtension="jpg",
                    ImageUrl=this.ImageProduct[randomNumbers[2]],
                    ProductId=product.Id,
                    OrderItem=3,
                    PublicId="test"
                },
                new()
                {
                    ImageExtension="jpg",
                    ImageUrl=this.ImageProduct[randomNumbers[3]],
                    ProductId=product.Id,
                    OrderItem=4,
                    PublicId="test"
                },
            };
            return imageProducts;
        }
        private List<Banner> BannerInit()
        {
            List<Banner> banner = new List<Banner>()
            {
                new()
                {
                    Title="What is Lorem Ipsum?",
                    Description="Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
                    " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s," +
                    " when an unknown printer took a galley of type and scrambled it to make a type" +
                    " specimen book. It has survived not only five centuries, but also the leap into" +
                    " electronic typesetting, remaining essentially unchanged. It was popularised in " +
                    "the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, " +
                    "and more recently with desktop publishing software like Aldus PageMaker including " +
                    "versions of Lorem Ipsum.",
                    IsVisible=true,
                    LogoImageUrl="https://images.pexels.com/photos/2876511/pexels-photo-2876511.jpeg?auto=compress&cs=tinysrgb&w=600"
                },
                new()
                {
                    Title="What is Lorem Ipsum?",
                    Description="Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
                    " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s," +
                    " when an unknown printer took a galley of type and scrambled it to make a type" +
                    " specimen book. It has survived not only five centuries, but also the leap into" +
                    " electronic typesetting, remaining essentially unchanged. It was popularised in " +
                    "the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, " +
                    "and more recently with desktop publishing software like Aldus PageMaker including " +
                    "versions of Lorem Ipsum.",
                    IsVisible=true,
                    LogoImageUrl="https://images.pexels.com/photos/1379627/pexels-photo-1379627.jpeg?auto=compress&cs=tinysrgb&w=600"
                },
                new()
                {
                    Title="What is Lorem Ipsum?",
                    Description="Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
                    " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s," +
                    " when an unknown printer took a galley of type and scrambled it to make a type" +
                    " specimen book. It has survived not only five centuries, but also the leap into" +
                    " electronic typesetting, remaining essentially unchanged. It was popularised in " +
                    "the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, " +
                    "and more recently with desktop publishing software like Aldus PageMaker including " +
                    "versions of Lorem Ipsum.",
                    IsVisible=true,
                    LogoImageUrl="https://images.pexels.com/photos/1191548/pexels-photo-1191548.jpeg?auto=compress&cs=tinysrgb&w=600"
                },
                new()
                {
                    Title="What is Lorem Ipsum?",
                    Description="Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
                    " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s," +
                    " when an unknown printer took a galley of type and scrambled it to make a type" +
                    " specimen book. It has survived not only five centuries, but also the leap into" +
                    " electronic typesetting, remaining essentially unchanged. It was popularised in " +
                    "the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, " +
                    "and more recently with desktop publishing software like Aldus PageMaker including " +
                    "versions of Lorem Ipsum.",
                    IsVisible=false,
                    LogoImageUrl="https://images.pexels.com/photos/6674032/pexels-photo-6674032.jpeg?auto=compress&cs=tinysrgb&w=600"
                },
                new()
                {
                    Title="What is Lorem Ipsum?",
                    Description="Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
                    " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s," +
                    " when an unknown printer took a galley of type and scrambled it to make a type" +
                    " specimen book. It has survived not only five centuries, but also the leap into" +
                    " electronic typesetting, remaining essentially unchanged. It was popularised in " +
                    "the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, " +
                    "and more recently with desktop publishing software like Aldus PageMaker including " +
                    "versions of Lorem Ipsum.",
                    IsVisible=true,
                    LogoImageUrl="https://images.pexels.com/photos/1424239/pexels-photo-1424239.jpeg?auto=compress&cs=tinysrgb&w=600"
                },
                new()
                {
                    Title="What is Lorem Ipsum?",
                    Description="Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
                    " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s," +
                    " when an unknown printer took a galley of type and scrambled it to make a type" +
                    " specimen book. It has survived not only five centuries, but also the leap into" +
                    " electronic typesetting, remaining essentially unchanged. It was popularised in " +
                    "the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, " +
                    "and more recently with desktop publishing software like Aldus PageMaker including " +
                    "versions of Lorem Ipsum.",
                    IsVisible=true,
                    LogoImageUrl="https://images.pexels.com/photos/2092828/pexels-photo-2092828.jpeg?auto=compress&cs=tinysrgb&w=600"
                },
                new()
                {
                    Title="What is Lorem Ipsum?",
                    Description="Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
                    " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s," +
                    " when an unknown printer took a galley of type and scrambled it to make a type" +
                    " specimen book. It has survived not only five centuries, but also the leap into" +
                    " electronic typesetting, remaining essentially unchanged. It was popularised in " +
                    "the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, " +
                    "and more recently with desktop publishing software like Aldus PageMaker including " +
                    "versions of Lorem Ipsum.",
                    IsVisible=true,
                    LogoImageUrl="https://images.pexels.com/photos/925263/pexels-photo-925263.jpeg?auto=compress&cs=tinysrgb&w=600"
                },
            };
            return banner;
        }
        private List<Brand> BrandsInit()
        {
            List<Brand> vegetableBrands = new List<Brand>()
            {
                new Brand
                {
                    Id = Guid.NewGuid(),
                    Name = "Fresh & Green",
                    UrlSlug = "fresh-green",
                    Description = "Providing fresh and high-quality vegetables directly from farms to your table.",
                    Image = "https://images.pexels.com/photos/755992/pexels-photo-755992.jpeg?auto=compress&cs=tinysrgb&w=600" 
                },
                new Brand
                {
                    Id = Guid.NewGuid(),
                    Name = "Organic Valley",
                    UrlSlug = "organic-valley",
                    Description = "Offering a wide range of organic vegetables grown sustainably and without harmful chemicals.",
                    Image = "https://images.pexels.com/photos/104842/bmw-vehicle-ride-bike-104842.jpeg?auto=compress&cs=tinysrgb&w=600" 
                },
                new Brand
                {
                    Id = Guid.NewGuid(),
                    Name = "Fruid",
                    UrlSlug = "Fruid",
                    Description = "Steamed vegetables packed with freshness and nutrients for a healthy and tasty side dish.",
                    Image = "https://images.pexels.com/photos/2529148/pexels-photo-2529148.jpeg?auto=compress&cs=tinysrgb&w=600"
                },
                new Brand
                {
                    Id = Guid.NewGuid(),
                    Name = "Oganic",
                    UrlSlug = "oganic",
                    Description = "Steamed vegetables packed with freshness and nutrients for a healthy and tasty side dish.",
                    Image = "https://images.pexels.com/photos/2983100/pexels-photo-2983100.jpeg?auto=compress&cs=tinysrgb&w=600"
                },
                new Brand
                {
                    Id = Guid.NewGuid(),
                    Name = "Heald",
                    UrlSlug = "heald",
                    Description = "Steamed vegetables packed with freshness and nutrients for a healthy and tasty side dish.",
                    Image = "https://images.pexels.com/photos/3689532/pexels-photo-3689532.jpeg?auto=compress&cs=tinysrgb&w=600"
                },
                new Brand
                {
                    Id = Guid.NewGuid(),
                    Name = "Anada",
                    UrlSlug = "anada",
                    Description = "Steamed vegetables packed with freshness and nutrients for a healthy and tasty side dish.",
                    Image = "https://cdn.shoplightspeed.com/shops/643839/themes/15892/v/897839/assets/first-image-1.jpg?20240319193340"
                }
            };
            return vegetableBrands;
        }
        private List<Categories> CategoryParrents()
        {
            List<Categories> subCategories = new List<Categories>()
            {
                new Categories() { Id = Guid.NewGuid(), Name = "Water Spinach", Description = "Water spinach is a popular green vegetable in Vietnam.", Image = "https://images.pexels.com/photos/26861260/pexels-photo-26861260/free-photo-of-nh-ng-ng-i-d-ng-ph-t-ng-v-n.jpeg?auto=compress&cs=tinysrgb&w=600&lazy=load", UrlSlug = "water-spinach" },
                new Categories() { Id = Guid.NewGuid(), Name = "Napa Cabbage", Description = "Napa cabbage is a type of Chinese cabbage, commonly used to make kimchi.", Image = "https://images.pexels.com/photos/17514564/pexels-photo-17514564/free-photo-of-phong-c-nh-nh-ng-dam-may-th-i-ti-t-d-i.jpeg?auto=compress&cs=tinysrgb&w=600&lazy=load", UrlSlug = "napa-cabbage" },
                new Categories() { Id = Guid.NewGuid(), Name = "Broccoli", Description = "Broccoli is a green vegetable that is often eaten steamed or raw.", Image = "https://images.pexels.com/photos/68507/spring-flowers-flowers-collage-floral-68507.jpeg?auto=compress&cs=tinysrgb&w=600", UrlSlug = "broccoli" },
                new Categories() { Id = Guid.NewGuid(), Name = "Spinach", Description = "Spinach is a leafy green vegetable that is rich in iron.", Image = "https://images.pexels.com/photos/250716/pexels-photo-250716.jpeg?auto=compress&cs=tinysrgb&w=600", UrlSlug = "spinach" },
                new Categories() { Id = Guid.NewGuid(), Name = "Carrot", Description = "Carrots are root vegetables that are often orange in color.", Image = "https://images.pexels.com/photos/1058836/pexels-photo-1058836.jpeg?auto=compress&cs=tinysrgb&w=600", UrlSlug = "carrot" },
                new Categories() { Id = Guid.NewGuid(), Name = "Tomato", Description = "Tomatoes are red fruits that are commonly used in salads and cooking.", Image = "https://images.pexels.com/photos/673857/pexels-photo-673857.jpeg?auto=compress&cs=tinysrgb&w=600", UrlSlug = "tomato" },
                new Categories() { Id = Guid.NewGuid(), Name = "Cucumber", Description = "Cucumbers are long, green vegetables that are often used in salads.", Image = "https://images.pexels.com/photos/235683/pexels-photo-235683.jpeg?auto=compress&cs=tinysrgb&w=600", UrlSlug = "cucumber" },
                new Categories() { Id = Guid.NewGuid(), Name = "Bell Pepper", Description = "Bell peppers are colorful vegetables that are often used in cooking.", Image = "https://images.pexels.com/photos/409800/pexels-photo-409800.jpeg?auto=compress&cs=tinysrgb&w=600", UrlSlug = "bell-pepper" },
                new Categories() { Id = Guid.NewGuid(), Name = "Zucchini", Description = "Zucchini is a type of summer squash that is often used in cooking.", Image = "https://images.pexels.com/photos/1408199/pexels-photo-1408199.jpeg?auto=compress&cs=tinysrgb&w=600", UrlSlug = "zucchini" },
                new Categories() { Id = Guid.NewGuid(), Name = "Lettuce", Description = "Lettuce is a leafy green vegetable that is commonly used in salads.", Image = "https://images.pexels.com/photos/1235742/pexels-photo-1235742.jpeg?auto=compress&cs=tinysrgb&w=600", UrlSlug = "lettuce" }
            };
            return subCategories;
        }
        private List<Product> ProductInit
            (
                List<Categories> categories,
                List<Brand> brands
            )
        {
            #region
            List<Product> products = new List<Product>()
            {
               new Product
                {
                    Name = "Apple",
                    Description = "Fresh and juicy red apples.",
                    Price = 1.20M,
                    UrlSlug = "apple",
                    Discount = 10,
                    CategoryId=categories[0].Id,
                    BrandId=brands[0].Id
                },
                new Product
                {
                    Name = "Banana",
                    Description = "Ripe bananas rich in potassium.",
                    Price = 0.50M,
                    UrlSlug = "banana",
                    Discount = null,
                    CategoryId=categories[0].Id,
                    BrandId=brands[0].Id
                },
                new Product
                {
                    Name = "Orange",
                    Description = "Sweet and tangy oranges, perfect for juicing.",
                    Price = 0.80M,
                    UrlSlug = "orange",
                    Discount = 5,
                    CategoryId=categories[0].Id,
                    BrandId=brands[0].Id
                },
                new Product
                {
                    Name = "Strawberry",
                    Description = "Fresh strawberries, perfect for desserts.",
                    Price = 2.00M,
                    UrlSlug = "strawberry",
                    Discount = 15,
                    CategoryId=categories[0].Id,
                    BrandId=brands[0].Id
                },
                new Product
                {
                    Name = "Grapes",
                    Description = "Seedless grapes, great for snacking.",
                    Price = 2.50M,
                    UrlSlug = "grapes",
                    Discount = null,
                    CategoryId=categories[0].Id,
                    BrandId=brands[0].Id
                },
                new Product
                {
                    Name = "Mango",
                    Description = "Sweet and ripe mangoes, full of tropical flavor.",
                    Price = 1.50M,
                    UrlSlug = "mango",
                    Discount = 20,
                    CategoryId=categories[1].Id,
                    BrandId=brands[1].Id
                },
                new Product
                {
                    Name = "Blueberry",
                    Description = "Fresh blueberries, packed with antioxidants.",
                    Price = 3.00M,
                    UrlSlug = "blueberry",
                    Discount = null,
                    CategoryId=categories[1].Id,
                    BrandId=brands[1].Id
                },
                new Product
                {
                    Name = "Watermelon",
                    Description = "Refreshing watermelon, perfect for hot days.",
                    Price = 5.00M,
                    UrlSlug = "watermelon",
                    Discount = 25,
                    CategoryId=categories[1].Id,
                    BrandId=brands[1].Id
                },
                new Product
                {
                    Name = "Pineapple",
                    Description = "Sweet and tangy pineapples, great for desserts and juices.",
                    Price = 3.50M,
                    UrlSlug = "pineapple",
                    Discount = null,
                    CategoryId=categories[1].Id,
                    BrandId=brands[1].Id
                },
                new Product
                {
                    Name = "Cherry",
                    Description = "Juicy and sweet cherries, perfect for snacking.",
                    Price = 4.00M,
                    UrlSlug = "cherry",
                    Discount = 10,
                    CategoryId=categories[1].Id,
                    BrandId=brands[1].Id
                },
                 new Product
                {
                    Name = "Peach",
                    Description = "Sweet and juicy peaches, perfect for eating fresh.",
                    Price = 2.20M,
                    UrlSlug = "peach",
                    Discount = 5,
                    CategoryId=categories[2].Id,
                    BrandId=brands[2].Id
                },
                new Product
                {
                    Name = "Cucumber",
                    Description = "Crisp and refreshing cucumbers, great for salads.",
                    Price = 1.00M,
                    UrlSlug = "cucumber",
                    Discount = null,
                    CategoryId=categories[2].Id,
                    BrandId=brands[2].Id
                },
                new Product
                {
                    Name = "Tomato",
                    Description = "Ripe and juicy tomatoes, perfect for salads and cooking.",
                    Price = 1.50M,
                    UrlSlug = "tomato",
                    Discount = 10,
                    CategoryId=categories[2].Id,
                    BrandId=brands[2].Id
                },
                new Product
                {
                    Name = "Carrot",
                    Description = "Crunchy and sweet carrots, great for snacking and cooking.",
                    Price = 1.20M,
                    UrlSlug = "carrot",
                    Discount = 5,
                    CategoryId=categories[2].Id,
                    BrandId=brands[2].Id
                },
                new Product
                {
                    Name = "Lettuce",
                    Description = "Fresh and crisp lettuce, perfect for salads.",
                    Price = 1.00M,
                    UrlSlug = "lettuce",
                    Discount = null,
                    CategoryId=categories[2].Id,
                    BrandId=brands[2].Id
                },
                new Product
                {
                    Name = "Bell Pepper",
                    Description = "Colorful bell peppers, great for salads and cooking.",
                    Price = 2.00M,
                    UrlSlug = "bell-pepper",
                    Discount = 10,
                    CategoryId=categories[3].Id,
                    BrandId=brands[3].Id
                },
                new Product
                {
                    Name = "Broccoli",
                    Description = "Fresh broccoli, packed with nutrients.",
                    Price = 2.50M,
                    UrlSlug = "broccoli",
                    Discount = null,
                    CategoryId=categories[3].Id,
                    BrandId=brands[3].Id
                },
                new Product
                {
                    Name = "Cauliflower",
                    Description = "Versatile cauliflower, great for roasting and mashing.",
                    Price = 2.30M,
                    UrlSlug = "cauliflower",
                    Discount = 15,
                    CategoryId=categories[3].Id,
                    BrandId=brands[3].Id
                },
                new Product
                {
                    Name = "Spinach",
                    Description = "Fresh spinach leaves, packed with iron and vitamins.",
                    Price = 1.80M,
                    UrlSlug = "spinach",
                    Discount = null,
                    CategoryId=categories[3].Id,
                    BrandId=brands[3].Id
                },
                new Product
                {
                    Name = "Potato",
                    Description = "Versatile potatoes, great for baking, mashing, and frying.",
                    Price = 1.00M,
                    UrlSlug = "potato",
                    Discount = 20,
                    CategoryId=categories[3].Id,
                    BrandId=brands[3].Id
                },
                new Product
                {
                    Name = "Onion",
                    Description = "Flavorful onions, essential for many dishes.",
                    Price = 1.00M,
                    UrlSlug = "onion",
                    Discount = null,
                    CategoryId=categories[4].Id,
                    BrandId=brands[4].Id
                },
                new Product
                {
                    Name = "Garlic",
                    Description = "Aromatic garlic, perfect for adding flavor to dishes.",
                    Price = 0.80M,
                    UrlSlug = "garlic",
                    Discount = 5,
                    CategoryId=categories[4].Id,
                    BrandId=brands[4].Id
                },
                new Product
                {
                    Name = "Avocado",
                    Description = "Creamy avocados, perfect for guacamole and salads.",
                    Price = 2.50M,
                    UrlSlug = "avocado",
                    Discount = 10,
                    CategoryId=categories[4].Id,
                    BrandId=brands[4].Id
                },
                new Product
                {
                    Name = "Zucchini",
                    Description = "Versatile zucchini, great for grilling and baking.",
                    Price = 1.50M,
                    UrlSlug = "zucchini",
                    Discount = null,
                    CategoryId=categories[4].Id,
                    BrandId=brands[4].Id
                },
                new Product
                {
                    Name = "Eggplant",
                    Description = "Rich and flavorful eggplants, perfect for roasting and grilling.",
                    Price = 2.00M,
                    UrlSlug = "eggplant",
                    Discount = 10,
                    CategoryId=categories[4].Id,
                    BrandId=brands[4].Id
                },
                new Product
                {
                    Name = "Pumpkin",
                    Description = "Sweet pumpkins, great for pies and soups.",
                    Price = 3.00M,
                    UrlSlug = "pumpkin",
                    Discount = 15,
                    CategoryId=categories[5].Id,
                    BrandId=brands[5].Id
                },
                new Product
                {
                    Name = "Lemon",
                    Description = "Tangy lemons, perfect for adding flavor to dishes.",
                    Price = 0.50M,
                    UrlSlug = "lemon",
                    Discount = 5,
                    CategoryId=categories[5].Id,
                    BrandId=brands[5].Id
                },
                new Product
                {
                    Name = "Lime",
                    Description = "Zesty limes, great for drinks and cooking.",
                    Price = 0.50M,
                    UrlSlug = "lime",
                    Discount = null,
                    CategoryId=categories[5].Id,
                    BrandId=brands[5].Id
                },
                new Product
                {
                    Name = "Pear",
                    Description = "Sweet and juicy pears, perfect for snacking.",
                    Price = 2.00M,
                    UrlSlug = "pear",
                    Discount = 10,
                    CategoryId=categories[5].Id,
                    BrandId=brands[5].Id
                },
                new Product
                {
                    Name = "Cabbage",
                    Description = "Crunchy cabbage, great for salads and cooking.",
                    Price = 1.20M,
                    UrlSlug = "cabbage",
                    Discount = 5,
                    CategoryId=categories[5].Id,
                    BrandId=brands[5].Id
                }
            };
            #endregion
            return products;
        }


        static int[] GenerateUniqueRandomNumbers(int max, int count)
        {
            if (count > max)
            {
                throw new ArgumentException("Count cannot be greater than the maximum value.");
            }

            Random random = new Random();
            List<int> uniqueNumbers = new List<int>();

            while (uniqueNumbers.Count < count)
            {
                int number = random.Next(max);
                if (!uniqueNumbers.Contains(number))
                {
                    uniqueNumbers.Add(number);
                }
            }

            return uniqueNumbers.ToArray();
        }

    }
}