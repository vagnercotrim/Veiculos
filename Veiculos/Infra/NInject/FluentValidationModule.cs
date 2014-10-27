using System.Reflection;
using System.Web.Mvc;
using FluentValidation;
using FluentValidation.Mvc;
using Ninject.Modules;
using Veiculos.Infra.FluentValidation;
using Veiculos.Models.Validation;

namespace Veiculos.Infra.NInject
{
    public class FluentValidationModule : NinjectModule
    {
        public override void Load()
        {
            NinjectValidatorFactory ninjectValidatorFactory = new NinjectValidatorFactory(Kernel);
            FluentValidationModelValidatorProvider.Configure(x => x.ValidatorFactory = ninjectValidatorFactory);
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly())
                           .ForEach(match => Kernel.Bind(match.InterfaceType)
                                                   .To(match.ValidatorType));

            Kernel.Bind<VeiculoValidation>().To<VeiculoValidation>();
        }
    }
}