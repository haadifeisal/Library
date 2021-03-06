﻿using AutoMapper;
using Library.WebApi.DataTransferObject;
using Library.WebApi.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.Controller
{
    [ApiController]
    [Route("v1/[controller]")]
    public class LibraryItemController : ControllerBase
    {
        private readonly ILibraryItemService _libraryItemService;
        private readonly IMapper _mapper;

        public LibraryItemController(ILibraryItemService libraryItemService, IMapper mapper)
        {
            _libraryItemService = libraryItemService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a collection of LibraryItems.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<LibraryItemResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetCollectionOfLibraryItem([FromQuery] bool sortByType)
        {
            var libraryItemCollection = _libraryItemService.GetCollectionOfLibraryItems(sortByType);

            if (!libraryItemCollection.Any())
            {
                return NoContent();
            }

            var mappedResult = _mapper.Map<List<LibraryItemResponseDto>>(libraryItemCollection);

            return Ok(mappedResult);
        }

        /// <summary>
        /// Get a libary item.
        /// </summary>
        /// <param name="libraryItemId"></param>
        /// <returns></returns>
        [HttpGet("{libraryItemId}")]
        [ProducesResponseType(typeof(LibraryItemResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetLibraryItem([FromRoute] int libraryItemId)
        {
            var libraryItem = _libraryItemService.GetLibraryItem(libraryItemId);

            if (libraryItem == null)
            {
                return NotFound();
            }

            var mappedResult = _mapper.Map<LibraryItemResponseDto>(libraryItem);

            return Ok(mappedResult);
        }

        /// <summary>
        /// Create a book library item.
        /// </summary>
        /// <param name="bookLibraryItemRequestDto"></param>
        /// <returns></returns>
        [HttpPost("book")]
        [ProducesResponseType(typeof(BookLibraryItemResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult CreateBookLibraryItem([FromBody] BookLibraryItemRequestDto bookLibraryItemRequestDto)
        {
            var libraryItem = _libraryItemService.CreateBookLibraryItem(bookLibraryItemRequestDto);

            if (libraryItem == null)
            {
                return UnprocessableEntity();
            }

            var mappedResult = _mapper.Map<BookLibraryItemResponseDto>(libraryItem);

            return Ok(mappedResult);
        }

        /// <summary>
        /// Create a dvd library item.
        /// </summary>
        /// <param name="dvdLibraryItemRequestDto"></param>
        /// <returns></returns>
        [HttpPost("dvd")]
        [ProducesResponseType(typeof(DvdLibraryItemResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult CreateDvdLibraryItem([FromBody] DvdLibraryItemRequestDto dvdLibraryItemRequestDto)
        {
            var libraryItem = _libraryItemService.CreateDvdLibraryItem(dvdLibraryItemRequestDto);

            if (libraryItem == null)
            {
                return UnprocessableEntity();
            }

            var mappedResult = _mapper.Map<DvdLibraryItemResponseDto>(libraryItem);

            return Ok(mappedResult);
        }

        /// <summary>
        /// Create audio book library item.
        /// </summary>
        /// <param name="audioBookLibraryItemRequestDto"></param>
        /// <returns></returns>
        [HttpPost("audiobook")]
        [ProducesResponseType(typeof(AudioBookLibraryItemResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult CreateAudioBookLibraryItem([FromBody] AudioBookLibraryItemRequestDto audioBookLibraryItemRequestDto)
        {
            var libraryItem = _libraryItemService.CreateAudioBookLibraryItem(audioBookLibraryItemRequestDto);

            if (libraryItem == null)
            {
                return UnprocessableEntity();
            }

            var mappedResult = _mapper.Map<AudioBookLibraryItemResponseDto>(libraryItem);

            return Ok(mappedResult);
        }

        /// <summary>
        /// Create reference book library item.
        /// </summary>
        /// <param name="referenceBookLibraryItemRequestDto"></param>
        /// <returns></returns>
        [HttpPost("referencebook")]
        [ProducesResponseType(typeof(ReferenceBookLibraryItemResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult CreateReferenceBookLibraryItem([FromBody] ReferenceBookLibraryItemRequestDto referenceBookLibraryItemRequestDto)
        {
            var libraryItem = _libraryItemService.CreateReferenceBookLibraryItem(referenceBookLibraryItemRequestDto);

            if (libraryItem == null)
            {
                return UnprocessableEntity();
            }

            var mappedResult = _mapper.Map<ReferenceBookLibraryItemResponseDto>(libraryItem);

            return Ok(mappedResult);
        }

        /// <summary>
        /// Borrow a library item.
        /// </summary>
        /// <param name="borrowLibraryItemRequestDto"></param>
        /// <returns></returns>
        [HttpPut("borrow")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult BorrowLibraryItem([FromBody] BorrowLibraryItemRequestDto borrowLibraryItemRequestDto)
        {
            var borrowLibraryItem = _libraryItemService.BorrowLibraryItem(borrowLibraryItemRequestDto);

            if (!borrowLibraryItem)
            {
                return UnprocessableEntity();
            }

            return Ok(borrowLibraryItem);
        }


        /// <summary>
        /// Check in library item.
        /// </summary>
        /// <param name="libraryItemId"></param>
        /// <returns></returns>
        [HttpPut("checkin/{libraryItemId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult CheckInLibraryItem([FromRoute] int libraryItemId)
        {
            var checkInLibraryItem = _libraryItemService.CheckInLibraryItem(libraryItemId);

            if (!checkInLibraryItem)
            {
                return UnprocessableEntity();
            }

            return Ok(checkInLibraryItem);
        }

        /// <summary>
        /// Edit Book library item.
        /// </summary>
        /// <param name="libraryItemId"></param>
        /// <param name="bookLibraryItemRequestDto"></param>
        /// <returns></returns>
        [HttpPut("book/{libraryItemId}")]
        [ProducesResponseType(typeof(BookLibraryItemResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult EditBookLibraryItem([FromRoute] int libraryItemId, [FromBody] BookLibraryItemRequestDto bookLibraryItemRequestDto)
        {
            var editBook = _libraryItemService.UpdateBookLibraryItem(libraryItemId, bookLibraryItemRequestDto);

            if (editBook == null)
            {
                return UnprocessableEntity();
            }

            var mappedResult = _mapper.Map<BookLibraryItemResponseDto>(editBook);

            return Ok(mappedResult);
        }

        /// <summary>
        /// Edit Dvd library item.
        /// </summary>
        /// <param name="libraryItemId"></param>
        /// <param name="dvdLibraryItemRequestDto"></param>
        /// <returns></returns>
        [HttpPut("dvd/{libraryItemId}")]
        [ProducesResponseType(typeof(DvdLibraryItemResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult EditDvdLibraryItem([FromRoute] int libraryItemId, [FromBody] DvdLibraryItemRequestDto dvdLibraryItemRequestDto)
        {
            var editDvd = _libraryItemService.UpdateDvdLibraryItem(libraryItemId, dvdLibraryItemRequestDto);

            if (editDvd == null)
            {
                return UnprocessableEntity();
            }

            var mappedResult = _mapper.Map<DvdLibraryItemResponseDto>(editDvd);

            return Ok(mappedResult);
        }

        /// <summary>
        /// Edit audio book library item.
        /// </summary>
        /// <param name="libraryItemId"></param>
        /// <param name="audioBookLibraryItemRequestDto"></param>
        /// <returns></returns>
        [HttpPut("audiobook/{libraryItemId}")]
        [ProducesResponseType(typeof(AudioBookLibraryItemResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult EditAudioBookLibraryItem([FromRoute] int libraryItemId, [FromBody] AudioBookLibraryItemRequestDto audioBookLibraryItemRequestDto)
        {
            var editAudioBook = _libraryItemService.UpdateAudioBookLibraryItem(libraryItemId, audioBookLibraryItemRequestDto);

            if (editAudioBook == null)
            {
                return UnprocessableEntity();
            }

            var mappedResult = _mapper.Map<AudioBookLibraryItemResponseDto>(editAudioBook);

            return Ok(mappedResult);
        }

        /// <summary>
        /// Edit reference book library item.
        /// </summary>
        /// <param name="libraryItemId"></param>
        /// <param name="referenceBookLibraryItemRequestDto"></param>
        /// <returns></returns>
        [HttpPut("referencebook/{libraryItemId}")]
        [ProducesResponseType(typeof(ReferenceBookLibraryItemResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult EditReferenceBookLibraryItem([FromRoute] int libraryItemId, [FromBody] ReferenceBookLibraryItemRequestDto referenceBookLibraryItemRequestDto)
        {
            var editReferenceBook = _libraryItemService.UpdateReferenceBookLibraryItem(libraryItemId, referenceBookLibraryItemRequestDto);

            if (editReferenceBook == null)
            {
                return UnprocessableEntity();
            }

            var mappedResult = _mapper.Map<ReferenceBookLibraryItemResponseDto>(editReferenceBook);

            return Ok(mappedResult);
        }

        /// <summary>
        /// Delete a library item. PS a library item that is borrowed by a user can't be deleted untill the user returns the library item.
        /// </summary>
        /// <param name="libraryItemId"></param>
        /// <returns></returns>
        [HttpDelete("{libraryItemId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult DeleteLibraryItem([FromRoute] int libraryItemId)
        {
            var deleteLibraryItem = _libraryItemService.DeleteLibraryItem(libraryItemId);

            if (!deleteLibraryItem)
            {
                return UnprocessableEntity();
            }

            return Ok(deleteLibraryItem);
        }

    }
}
