using Application.UseCases.ExecuteExpression;
using Application.UseCases.ExecuteExpression.Abstractions;
using Application.UseCases.ExecuteExpression.Ports;
using FluentAssertions;
using Moq.AutoMock;

namespace UnitTest.UseCases.ExecuteExpression;

public class ExecuteExpressionTests
{
    private readonly IExecuteExpressionUseCase _sut;

    public ExecuteExpressionTests()
    { 
        var mocker = new AutoMocker();
        _sut = mocker.CreateInstance<ExecuteExpressionUseCase>();
    }

    private static ExpressionInput GetInput(string expression) =>
        new ExpressionInput(Guid.NewGuid(), expression);

    [Theory]
    [MemberData(nameof(GetMathData))]
    public async Task ShouldExecuteMathExpressions(string expression, decimal expected)
    {
        //Arrange
        var input = GetInput(expression);

        //Act
        var result = await _sut.Execute(input);

        //Assert
        result.Value.Should().Be(expected);
    }

    private static object[] GetMathData() => new object[] 
    {
        new object[] {"1+3", 4},
        new object[] {"10*1+2", 12},
        new object[] {"-1+3", 2},
        new object[] {"3 - 1.5", 1.5},
        new object[] {"7 / 2", 3.5},
        new object[] {"1.25 * 5.80", 7.25},
        new object[] {"2 ^ 3", 8},
        new object[] {"15 % 2", 1},
        new object[] {"(((3 + 5) + (6 / 2) - 1) / 2) ^ 2", 25}
    };

    [Theory]
    [MemberData(nameof(GetLogicalData))]
    public async Task ShouldExecuteLogicalExpressions(string expression, decimal expected)
    {
        //Arrange
        var input = GetInput(expression);

        //Act
        var result = await _sut.Execute(input);

        //Assert
        result.Value.Should().Be(expected);
    }

    private static object[] GetLogicalData() => new object[]
    {
        new object[] {@"SE 1 > 0
                        E 2 > 1
                        E 1 = 1
                        E 3 >= 2
                        E 4 >= 4
                        E 4 <= 4
                        E 5 <> 4
                        E (1 + 1 = 2) ENTAO
                            30000
                        SENAO
                            60000", 30000},

        new object[] {@"SE 1 > 1
                        OU -2 > -1 ENTAO
                            30000
                        SENAO
                            60000", 60000},

        new object[] {@"SE 1 < 0 e 2 < 1
                        ENTAO 30000
                        SENAO 60000", 60000},

        new object[] {@"/*Bloco de comentário*/
                        SE 1 < 0 e 2 < 1 ENTAO
                            30000
                        SENAO
                            60000", 60000},

        new object[] {@"SE 1 < 0 e 2 < 1 ENTAO
                            30000
                        SENAO
                            SE (2 + 2) > 3 ENTAO
                                27
                            SENAO
                                28", 27}
    };

    
    [Fact]
    public async Task ShouldExecuteNestedLogicalExpressions()
    {
        //Arrange
        var expression = @"SE 2 = 1 + 1 ENTAO
                            SE  5 = 3 + 2 ENTAO
                                SE 8 = 4 + 4 ENTAO
                                    2000
                                SENAO
                                    0
                            SENAO
                                0
                        SENAO
                            0";
        var expected = 2000;
        var input = GetInput(expression);

        //Act
        var result = await _sut.Execute(input);

        //Assert
        result.Value.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(GetDateData))]
    public async Task ShouldExecuteDateExpressions(string expression, decimal expected)
    {
        //Arrange
        var input = GetInput(expression);

        //Act
        var result = await _sut.Execute(input);

        //Assert
        result.Value.Should().Be(expected);
    }

    private static object[] GetDateData() => new object[]
    {
        new object[] {@"SE 06/10/2011 > 05/10/2010 ENTAO
                            1
                        SENAO
                            0", 1},

        // new object[] {@"06/10/2011 - 05/10/2010", 366},

        // new object[] {@"SE 06.10.2011 - 05.10.2010 >= 366
        //                 OU 06.10.2011 - 05.10.2010 - 1 = 365 ENTAO
        //                     1
        //                 SENAO
        //                     2", 1}
    };

    [Fact]
    public async Task ShouldExecuteEmptyExpressions()
    {
        //Arrange
        var input = GetInput(string.Empty);

        //Act
        var result = await _sut.Execute(input);

        //Assert
        result.Value.Should().BeNull();
    }

    [Fact]
    public async Task ShouldExecuteFunctionRandomExpression()
    {
        //Arrange
        var input = GetInput("Randomico(4)");
        var expected = new[] { 0, 1, 2, 3 };

        //Act
        var result = await _sut.Execute(input);

        //Assert
        result.Value.Should()
            .Match<int>(value => expected.Contains(value));
    }

}
