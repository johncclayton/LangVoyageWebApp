# Learning Endpoint Test Documentation

This directory contains comprehensive unit tests for the Learning Endpoint of the LangVoyage application. The tests are designed to be thorough, reusable, and maintainable.

## Test Structure

### Infrastructure Components

#### TestHelpers/
- **TestDataBuilder.cs**: Fluent API for creating test data objects with sensible defaults
- **HttpTestHelpers.cs**: Common HTTP testing utilities with JSON serialization support  
- **DatabaseStateAssertions.cs**: Database state verification helpers for asserting data integrity

#### LearningEndpoints/
- **LearningEndpointTestBase.cs**: Base class providing common setup, utilities, and test infrastructure
- **LearningEndpointGetTests.cs**: Tests for GET endpoints (progress and noun retrieval)
- **LearningEndpointPutTests.cs**: Tests for PUT endpoints (progress updates)
- **LearningEndpointDeleteTests.cs**: Tests for DELETE endpoints (progress deletion)
- **LearningEndpointIntegrationTests.cs**: Integration tests for complex workflows
- **LearningEndpointSimpleTests.cs**: Basic tests to validate infrastructure

## Test Coverage

### Learning Endpoint Operations Tested

1. **GET /learn/v1/{userId}/progress** - Learning progress statistics
   - Success scenarios with valid data
   - Empty progress handling
   - Progress distribution verification
   - Error scenarios (invalid users, HTTP status codes)
   - Performance testing

2. **GET /learn/v1/{userId}/noun** - Practice noun retrieval
   - User level filtering
   - Spaced repetition ordering
   - Consistent behavior with no progress
   - Error handling and edge cases
   - Performance testing

3. **PUT /learn/v1/{userId}/noun** - Progress updates
   - First-time practice scenarios
   - Spaced repetition logic (correct/incorrect answers)
   - TimeFrame progression and regression
   - Multi-user independence
   - Concurrency handling
   - Error scenarios and validation

4. **DELETE /learn/v1/{userId}/noun** - Progress deletion
   - Complete progress removal
   - Idempotent behavior
   - User isolation
   - Performance with large datasets
   - Recovery scenarios

### Test Categories

- **Functional Tests**: Verify correct business logic implementation
- **HTTP Tests**: Validate API contracts, status codes, and error handling
- **Edge Cases**: Boundary conditions, invalid inputs, and error scenarios
- **Performance Tests**: Response times and throughput under load
- **Concurrency Tests**: Thread safety and data integrity under concurrent access
- **Integration Tests**: Complex workflows spanning multiple operations

## Key Features

### Reusable Infrastructure
- Fluent test data builders for easy test setup
- Common assertion patterns for database state verification
- HTTP testing utilities with automatic JSON serialization
- Base test classes that can be extended for other endpoints

### Comprehensive Coverage
- **70+ tests** covering all Learning Endpoint operations
- Success and failure scenarios
- Edge cases and boundary conditions  
- Performance and stress testing
- Multi-user scenarios

### Test Isolation
- Each test starts with a clean database state
- Independent test execution (can run in any order)
- Proper cleanup and resource disposal

## Usage Examples

### Creating Test Data
```csharp
var user = TestDataBuilder.UserProfile()
    .WithId(1)
    .WithUsername("test-user")
    .WithLanguageLevel("B2")
    .Build();

var request = TestDataBuilder.NounProgressRequest()
    .WithNounId(123)
    .WithAnswerWasCorrect(true)
    .Build();
```

### HTTP Testing
```csharp
var nouns = await HttpTestHelpers.GetAsync<IList<LanguageNoun>>(client, "/learn/v1/1/noun");
var (response, data) = await HttpTestHelpers.GetWithStatusAsync<LanguageNoun>(client, url);
HttpTestHelpers.AssertStatusCode(response, HttpStatusCode.OK);
```

### Database Assertions
```csharp
await DatabaseAssertions.AssertUserExistsAsync(1, "test-user", "B2");
await DatabaseAssertions.AssertNounProgressExistsAsync(1, 123, expectedTimeFrame: 2);
await DatabaseAssertions.AssertUserHasNoProgressAsync(1);
```

## Extending for Other Endpoints

The test infrastructure is designed for reusability:

1. **Extend LearningEndpointTestBase** for similar REST endpoints
2. **Add new builders to TestDataBuilder** for additional models
3. **Extend DatabaseStateAssertions** for new entity verification
4. **Use HttpTestHelpers** for consistent HTTP testing patterns

## Running Tests

```bash
# Run all tests
dotnet test

# Run specific test class
dotnet test --filter "ClassName=TestServer.LearningEndpoints.LearningEndpointGetTests"

# Run specific test
dotnet test --filter "GetLearningProgress_WithValidUser_ReturnsSuccessWithValidStructure"

# List all available tests
dotnet test --list-tests
```

## Performance Considerations

- Tests include performance assertions to catch regressions
- Database operations are optimized for test speed
- Concurrent test execution is supported with proper isolation
- Large dataset tests verify scalability

This comprehensive test suite ensures the Learning Endpoint is robust, reliable, and maintainable while providing a solid foundation for testing other API endpoints.