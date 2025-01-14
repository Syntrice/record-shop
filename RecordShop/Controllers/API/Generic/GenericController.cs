﻿using Microsoft.AspNetCore.Mvc;
using RecordShop.Model;
using RecordShop.Services.Generic;
using RecordShop.Services.Response;

namespace RecordShop.Controllers.API.Generic
{
    public class GenericController<TIdentifiable> : ControllerBase where TIdentifiable : class, IIdentifiable
    {
        private readonly IGenericService<TIdentifiable> _genericService;

        public GenericController(IGenericService<TIdentifiable> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _genericService.GetEntities();

            switch (result.ResponseType)
            {
                case ServiceResponseType.NotFound:
                    return NotFound(result.Message);
                case ServiceResponseType.Success:
                    return Ok(result.Value);
                default:
                    return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _genericService.GetEntityById(id); 

            switch (result.ResponseType)
            {
                case ServiceResponseType.NotFound:
                    return NotFound(result.Message);
                case ServiceResponseType.Success:
                    return Ok(result.Value);
                default:
                    return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post(TIdentifiable entity)
        {
            var result = _genericService.InsertEntity(entity);

            switch (result.ResponseType)
            {
                case ServiceResponseType.Success:

                    if (result.Value == null)
                    {
                        return Created();
                    }

                    return Created(result.Value.Id.ToString(), result.Value);
                default:
                    return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _genericService.DeleteEntityById(id);

            switch (result.ResponseType)
            {
                case ServiceResponseType.NotFound:
                    return NotFound(result.Message);
                case ServiceResponseType.Success:
                    return NoContent();
                default:
                    return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, TIdentifiable entity)
        {
            var result = _genericService.UpdateEntity(entity);

            switch (result.ResponseType)
            {
                case ServiceResponseType.NotFound:
                    return NotFound(result.Message);
                case ServiceResponseType.Success:
                    return NoContent();
                default:
                    return BadRequest();
            }
        }
    }
}
