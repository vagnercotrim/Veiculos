﻿using FluentValidation;
using System;

namespace Veiculos.Models.Validation
{
    public class AutorizacaoCirculacaoValidation : AbstractValidator<AutorizacaoCirculacao>
    {

        public AutorizacaoCirculacaoValidation()
        {
            RuleFor(a => a.Numero).GreaterThan(0);
            RuleFor(a => a.Ano).GreaterThanOrEqualTo(DateTime.Now.Year - 1);
            RuleFor(a => a.Ano).LessThanOrEqualTo(DateTime.Now.Year);

            RuleFor(a => a.Inicio).LessThanOrEqualTo(a => a.Termino);
            RuleFor(a => a.Finalidade).NotEmpty();
            RuleFor(a => a.Observacao).NotEmpty();
            RuleFor(a => a.QuemAutorizou).NotNull();
        }

    }
}