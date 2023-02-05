using ECommerce.Application.ViewModels.Products;
using FluentValidation;

namespace ECommerce.Application.Validators.Products
{
    public class CreateProductViewModelValidator : AbstractValidator<CreateProductViewModel>
    {
        public CreateProductViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Lütfen ürün adını boş bırakmayınız.")
                .MinimumLength(5).MaximumLength(150).WithMessage("Ürün adı 5 karakterden az 150 karakterden fazla olamaz.");
            RuleFor(x => x.Stock).NotEmpty().NotNull().WithMessage("Lütfen ürün stok bilgisini boş bırakmayınız.")
                .Must(s => s >= 0).WithMessage("Stok bilgisi sıfırdan küçük olamaz.");
            RuleFor(x => x.Price).NotEmpty().NotNull().WithMessage("Lütfen ücret bilgisini boş bırakmayınız.")
                .Must(p => p >= 0).WithMessage("Fiyat bilgisi sıfırdan küçük olamaz.");
        }
    }
}
