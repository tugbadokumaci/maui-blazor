using System;
using MauiBlazor.Shared.Models;

namespace MauiBlazor.Mobile.Services;

public interface ILocalService
{
    Task<ResponseModel<string>> AddNewCardToLatestScanedCards(CardModel newScannedCard);
	Task<ResponseModel<List<int>>> GetLatestScanedCards();
}

