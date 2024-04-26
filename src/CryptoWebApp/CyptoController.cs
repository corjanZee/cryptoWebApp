using CryptoWebApp.Messages;
using Microsoft.AspNetCore.Mvc;

namespace CryptoWebApp;
[Route("api/")]
public class CyptoController(ICryptoCurrencyRepository cryptoCurrencyRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var results = await cryptoCurrencyRepository.GetAllAsync(cancellationToken);
        return Ok(results);
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

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItem(string id, CancellationToken cancellationToken)
    {
        var deletedItem = await cryptoCurrencyRepository.Delete(id, cancellationToken);
        if (deletedItem == null)
            return BadRequest($"There is no Crypto currency found with  id {id}");
        
        return Ok();
    }
}