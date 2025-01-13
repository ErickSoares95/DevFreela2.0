using DevFreela.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.UserQueries.GetUserById
{
    public class GetUserByidQuery : IRequest<ResultViewModel<UserItemViewModel>>
    {
        public GetUserByidQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
