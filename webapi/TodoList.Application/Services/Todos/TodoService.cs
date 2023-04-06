using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoList.Application.Services.Todos.Dto;
using TodoList.Core.Domain;
using TodoList.Core.Exceptions;
using TodoList.Persistence;

namespace TodoList.Application.Services.Todos
{
    public class TodoService : ITodoService
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<TodoService> _logger;
        public TodoService(IAppDbContext dbContext, IMapper mapper, ILogger<TodoService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TodoDto> CreateAsync(RequestCreateTask input, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Todo>(input);
            var lastOrder = await _dbContext.Todos.AsNoTracking()
                    .OrderBy(o => o.Id)
                    .Select(s => s.Order)
                    .LastOrDefaultAsync(cancellationToken);
            entity.Order = lastOrder + 1;
            await _dbContext.Todos.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TodoDto>(entity);
        }

        public async Task DeleteAsync(int todoId, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Todos.FirstOrDefaultAsync
                (x => x.Id == todoId, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException($"Task Id {todoId} not found");
            }

            _dbContext.Todos.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<TodoDto>> GetAsync(CancellationToken cancellationToken)
        {
            var results = await _dbContext.Todos.AsNoTracking()
                    .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<TodoDto>>(results);
        }

        public async Task<TodoDto> GetAsync(int todoId, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Todos.FirstOrDefaultAsync
                (x => x.Id == todoId, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException($"Task Id {todoId} not found");
            }

            return _mapper.Map<TodoDto>(entity);
        }

        public async Task<IEnumerable<ResponseTodoCompletion>> TaskCompletionAsync(IEnumerable<RequestTodoCompletion> input, CancellationToken cancellationToken)
        {
            var results = new List<ResponseTodoCompletion>();
            var ids = input.Select(s => s.Id).ToList();

            var entities = await _dbContext.Todos.
                Where(x => ids.Contains(x.Id))
                .ToListAsync(cancellationToken);

            foreach (var item in input)
            {
                var itemId = item.Id;
                var itemIsComplete = item.IsComplete;
                var saveResult = false;

                try
                {
                    var entity = entities.FirstOrDefault(x => x.Id == itemId);

                    if (entity is null)
                    {
                        throw new NotFoundException($"Task Id {itemId} not found");
                    }
                    
                    entity.Done = item.IsComplete;
                    _dbContext.Todos.Update(entity);
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    saveResult = true;
                }
                catch (Exception err)
                {
                    itemIsComplete = false;
                    _logger.LogError($"{err.Message} || {err.StackTrace}");
                }

                results.Add(new ResponseTodoCompletion
                {
                    Id = itemId,
                    IsComplete = itemIsComplete,
                    IsSuccess = saveResult,
                });
            }
            return results;
        }

        public async Task<TodoDto> UpdateAsync(int todoId, RequestUpdateTask input, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Todos.FirstOrDefaultAsync
               (x => x.Id == todoId, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException($"Task Id {todoId} not found");
            }

            entity = _mapper.Map(input, entity);
            _dbContext.Todos.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TodoDto>(entity);
        }
    }
}
