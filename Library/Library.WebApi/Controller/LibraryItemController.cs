using AutoMapper;
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
        public IActionResult GetCollectionOfLibraryItem()
        {
            var libraryItemCollection = _libraryItemService.GetCollectionOfLibraryItems();

            if (!libraryItemCollection.Any())
            {
                return NoContent();
            }

            var mappedResult = _mapper.Map<List<LibraryItemResponseDto>>(libraryItemCollection);

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

    }
}
