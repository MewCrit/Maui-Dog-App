using System.Data;
using System.Data.SqlClient;
using Dapper;
using dog_service.Models;
using Microsoft.AspNetCore.Mvc;

namespace dog_service.Controllers;

[ApiController]
[Route("dogo")]
public class DogController : ControllerBase
{
     private string ConnetionString ;

    private readonly ILogger<DogController> _logger;

    public DogController(ILogger<DogController> logger)
    {
        _logger = logger;
        ConnetionString = "Data Source=host.docker.internal,1433;Initial Catalog=Doggo;User ID=sa;Password=Abc1234567*;";
    }

    [HttpPost]
    [Route("dog")]
    public async Task<IActionResult> Create(DogParameter model)
    {
        bool result;

        var dogID = Guid.NewGuid().ToString();

        using (var connection = new SqlConnection(this.ConnetionString))
               result = Convert.ToBoolean(await connection.ExecuteAsync("sp_create_new_dog",
                        new
                        {
                          id = dogID, 
                          name  = model.Name, 
                          picture = model.Picture, 
                          breed = model.Breed,  
                          about = model.About, 
                          gender = model.Gender, 
                          birthday  = model.Birthday              
                        }, commandType: CommandType.StoredProcedure));

        if(!result)
            return StatusCode(500);

        return Created(string.Empty, new {
            StatusCode = 201,
            DogID = dogID
        });
    }

    
     [HttpGet]
     [Route("dog")]
    public async Task<IActionResult> Get()
    {
        List<ReadDogDto> doggies = new();
        using (var connection = new SqlConnection(this.ConnetionString))
         {
             dynamic list = await connection.QueryAsync("sp_read_dog",
                            commandType: CommandType.StoredProcedure) ?? new List<ReadDogDto>();

                foreach(var dog in list)
                {
                    doggies.Add(new ReadDogDto(
                         dog.id,
                         dog.name,
                         dog.picture,
                         dog.breed,
                         dog.about,
                         dog.gender
                    ));
                }
         }

        return Ok(doggies);
    }


    [HttpGet]
    [Route("dog/{id}")]
    public async Task<IActionResult> GetById(string id) 
    { 
         ReadDogDto doggy ;
         using (var connection = new SqlConnection(this.ConnetionString))
         {
             dynamic dog = await connection.QueryFirstOrDefaultAsync("sp_read_dog_by_id", 
                            new {id = id},
                            commandType: CommandType.StoredProcedure) ;

                 doggy = new ReadDogDto(
                         dog.id,
                         dog.name,
                         dog.picture,
                         dog.breed,
                         dog.about,
                         dog.gender
                    );
         }

        return Ok(doggy);
    }











}
