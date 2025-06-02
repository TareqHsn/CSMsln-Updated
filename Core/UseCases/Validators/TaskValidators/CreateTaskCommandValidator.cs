using CSM.Core.UseCases.Commands.TasksCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM.Core.UseCases.Validators.TaskValidators
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.Task)
                .NotNull()
                .WithMessage("Task cannot be null.");

            RuleFor(x => x.Task.Title)
                .NotEmpty()
                .WithMessage("Task title is required.")
                .MaximumLength(100)
                .WithMessage("Task title cannot exceed 100 characters.");

        }
    }
}
