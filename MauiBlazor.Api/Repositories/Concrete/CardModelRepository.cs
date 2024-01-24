using System;
using MauiBlazor.Api.Data;
using MauiBlazor.Api.Repositories.Abstract;
using MauiBlazor.Api.Repositories.Concrete.Common;
using MauiBlazor.Shared.Models;

namespace MauiBlazor.Api.Repositories.Concrete;

public class CardModelRepository : GenericRepository<CardModel>, ICardModelRepository
{
    public CardModelRepository(MyDbContext context) : base(context)
    {
    }
}
