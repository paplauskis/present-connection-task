using Microsoft.AspNetCore.Mvc;
using package_tracking_api.Domain.Dtos;
using package_tracking_api.Domain.Exceptions;
using package_tracking_api.Domain.Interfaces;
using package_tracking_api.Domain.Models;

namespace package_tracking_api.Controllers;

[ApiController]
[Route("/api/packages")]
public class PackageController : ControllerBase
{
    private readonly IPackageService _packageService;

    public PackageController(IPackageService packageService)
    {
        _packageService = packageService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<PackageDto>>> GetPackages()
    {
        try
        {
            var packages = await _packageService.GetPackagesAsync();
            
            return Ok(packages);
        }
        catch (PackageNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("create")]
    public async Task<ActionResult<PackageDto>> Create([FromBody] CreatePackageDto packageDto)
    {
        try
        {
            var package = await _packageService.CreatePackageAsync(packageDto);
            
            return Created($"/api/packages/{package.Id}", package);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<PackageDto>>>
        GetPackagesByStatus([FromQuery] PackageFilterDto packageFilterDto)
    {
        try
        {
            var packages = await _packageService.GetFilteredPackagesAsync(packageFilterDto);
            
            return Ok(packages);
        }
        catch (PackageNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpGet("by-tracking-number/{trackingNumber}")]
    public async Task<ActionResult<PackageDto>>
        GetPackageByTrackingNumber(string trackingNumber)
    {
        try
        {
            var packages = await _packageService.GetPackageByTrackingNumber(trackingNumber);
            
            return Ok(packages);
        }
        catch (PackageNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPatch("update-status")]
    public async Task<ActionResult<PackageDto>> UpdateStatus([FromBody] PackageUpdateStatusDto packageDto)
    {
        try
        {
            var package = await _packageService.UpdatePackageStatusAsync(packageDto);

            return Ok(package);
        }
        catch (PackageNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (InvalidPackageStatusTransitionException e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("statuses")]
    public ActionResult<IEnumerable<string>> GetAllStatuses()
    {
        var statuses = Enum.GetNames(typeof(PackageStatus));
        return Ok(statuses);
    }
}