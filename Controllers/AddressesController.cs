using ClothingStore.Api.Data;
using ClothingStore.Api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Controllers;

[ApiController]
[Route("api/addresses")]
public class AddressesController : ControllerBase
{
    private readonly EntityApiContext _context;

    public AddressesController(EntityApiContext context)
    {
        _context = context;
    }

    // GET: api/addresses?page=1&pageSize=10
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AddressDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AddressDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var addresss = await _context.Addresses
            .OrderBy(address => address.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return addresss.Select(AddressMapper.ToDto).ToList();
    }

    // GET: api/addresses/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AddressDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AddressDto>> GetById(int id)
    {
        var address = await _context.Addresses
            .FirstOrDefaultAsync(address => address.Id == id);

        if (address is null)
        {
            return NotFound();
        }

        return AddressMapper.ToDto(address);
    }

    // POST: api/addresses
    [HttpPost]
    [ProducesResponseType(typeof(AddressDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AddressDto>> Create(CreateAddressDto dto)
    {
        var address = AddressMapper.ToEntity(dto);

        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = address.Id }, AddressMapper.ToDto(address));
    }

    // PUT: api/addresses/5 — полное обновление
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateAddressDto dto)
    {
        var address = await _context.Addresses
            .FirstOrDefaultAsync(address => address.Id == id);

        if (address is null)
        {
            return NotFound();
        }

        address.UserId = dto.UserId;
        address.FullName = dto.FullName;
        address.Phone = dto.Phone;
        address.Country = dto.Country;
        address.Region = dto.Region;
        address.City = dto.City;
        address.Street = dto.Street;
        address.House = dto.House;
        address.Apartment = dto.Apartment;
        address.PostIndex = dto.PostIndex;
        address.IsDefault = dto.IsDefault;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/addresses/5 — частичное обновление (только переданные поля)
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id, PatchAddressDto dto)
    {
        var address = await _context.Addresses
            .FirstOrDefaultAsync(address => address.Id == id);

        if (address is null)
        {
            return NotFound();
        }

        var hasChanges = false;

        if (dto.UserId is not null)
        {
            address.UserId = dto.UserId.Value;
            hasChanges = true;
        }

        if (dto.FullName is not null)
        {
            address.FullName = dto.FullName;
            hasChanges = true;
        }

        if (dto.Phone is not null)
        {
            address.Phone = dto.Phone;
            hasChanges = true;
        }

        if (dto.Country is not null)
        {
            address.Country = dto.Country;
            hasChanges = true;
        }

        if (dto.Region is not null)
        {
            address.Region = dto.Region;
            hasChanges = true;
        }

        if (dto.City is not null)
        {
            address.City = dto.City;
            hasChanges = true;
        }

        if (dto.Street is not null)
        {
            address.Street = dto.Street;
            hasChanges = true;
        }

        if (dto.House is not null)
        {
            address.House = dto.House;
            hasChanges = true;
        }

        if (dto.Apartment is not null)
        {
            address.Apartment = dto.Apartment;
            hasChanges = true;
        }

        if (dto.PostIndex is not null)
        {
            address.PostIndex = dto.PostIndex;
            hasChanges = true;
        }

        if (dto.IsDefault is not null)
        {
            address.IsDefault = dto.IsDefault.Value;
            hasChanges = true;
        }

        if (hasChanges)
        {
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/addresses/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var address = await _context.Addresses
            .FirstOrDefaultAsync(address => address.Id == id);

        if (address is null)
        {
            return NotFound();
        }

        _context.Addresses.Remove(address);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}