import XCTest

import ganitTests

var tests = [XCTestCaseEntry]()
tests += ganitTests.allTests()
XCTMain(tests)
