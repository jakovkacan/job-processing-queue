using JobProcessingQueue.Consumers.DataPipelines;
using JobProcessingQueue.Consumers.DataPipelines.Strategies;
using JobProcessingQueue.Output;
using Moq;

namespace JobProcessingQueue.Tests.Consumers;

public class DataPipelineTests
    {
        [Fact]
        public void DataPipeline_Consume_WithTextOutput_CallsDatabaseStrategy()
        {
            // Arrange
            var mockDbStrategy = new Mock<IDatabaseStrategy>();
            mockDbStrategy.Setup(s => s.Save(It.IsAny<IJobOutput>()))
                         .Returns(Task.CompletedTask);

            var pipeline = new DataPipeline
            {
                DatabaseStrategy = mockDbStrategy.Object
            };

            var output = new TextOutput { Content = "Test data" };

            // Act
            pipeline.Consume(output);

            // Assert
            mockDbStrategy.Verify(s => s.Save(output), Times.Once);
        }

        [Fact]
        public void DataPipeline_Consume_WithTabularOutput_CallsDatabaseStrategy()
        {
            // Arrange
            var mockDbStrategy = new Mock<IDatabaseStrategy>();
            mockDbStrategy.Setup(s => s.Save(It.IsAny<IJobOutput>()))
                         .Returns(Task.CompletedTask);

            var pipeline = new DataPipeline
            {
                DatabaseStrategy = mockDbStrategy.Object
            };

            var output = new TabularOutput
            {
                Rows = new List<Dictionary<string, object>>()
            };

            // Act
            pipeline.Consume(output);

            // Assert
            mockDbStrategy.Verify(s => s.Save(output), Times.Once);
        }

        [Fact]
        public void DataPipeline_Consume_WithSequentialOutput_CallsDatabaseStrategy()
        {
            // Arrange
            var mockDbStrategy = new Mock<IDatabaseStrategy>();
            mockDbStrategy.Setup(s => s.Save(It.IsAny<IJobOutput>()))
                         .Returns(Task.CompletedTask);

            var pipeline = new DataPipeline
            {
                DatabaseStrategy = mockDbStrategy.Object
            };

            var output = new SequentialOutput
            {
                Values = new List<object> { 1, 2, 3 }
            };

            // Act
            pipeline.Consume(output);

            // Assert
            mockDbStrategy.Verify(s => s.Save(output), Times.Once);
        }

        [Fact]
        public void SQLDatabaseStrategy_HasConnectionString()
        {
            // Arrange
            var strategy = new SQLDatabaseStrategy
            {
                ConnectionString = "Server=localhost;Database=test;"
            };

            // Act & Assert
            Assert.Equal("Server=localhost;Database=test;", strategy.ConnectionString);
        }

        [Fact]
        public void NoSQLDatabaseStrategy_HasConnectionString()
        {
            // Arrange
            var strategy = new NoSQLDatabaseStrategy
            {
                ConnectionString = "mongodb://localhost:27017"
            };

            // Act & Assert
            Assert.Equal("mongodb://localhost:27017", strategy.ConnectionString);
        }
    }