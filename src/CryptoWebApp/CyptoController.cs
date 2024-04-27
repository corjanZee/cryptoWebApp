using CryptoWebApp.Messages;
using Microsoft.AspNetCore.Mvc;

namespace CryptoWebApp;

[Route("/")]
public class CyptoController(ICryptoCurrencyRepository cryptoCurrencyRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var results = await cryptoCurrencyRepository.GetAllAsync(cancellationToken);
        return Ok(results);
    }

    [HttpGet("search")]
    public async Task<ActionResult> Search(string code, string name, CancellationToken cancellationToken)
    {
        return Ok(await cryptoCurrencyRepository.GetAsync(code, name, cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult> CreateItem([FromBody] CryptoModel model, CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await cryptoCurrencyRepository.CreateAsync(model, cancellationToken));
        }
        catch (AlreadyExistException e)
        {
            return BadRequest($"Crypto with {e.CyCryptoModel.Code} and {e.CyCryptoModel.Name} already exist");
        }
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateItem([FromBody] CryptoModel cryptoModel, string id, CancellationToken cancellationToken)
    {
        var items = (await cryptoCurrencyRepository.GetAsync(cryptoModel.Code, cryptoModel.Name, cancellationToken))
            .Where(x => x.Id != id);
        if (items.Any())
            return BadRequest($"Cannot update item with id, because there already with either code with name '{cryptoModel.Code}' or with name {cryptoModel.Name}");

        return Ok(await cryptoCurrencyRepository.Update(id, cryptoModel, cancellationToken));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItem(string id, CancellationToken cancellationToken)
    {
        var deletedItem = await cryptoCurrencyRepository.Delete(id, cancellationToken);
        if (deletedItem == null)
            return BadRequest($"There is no Crypto currency found with  id {id}");

        return Ok();
    }
}