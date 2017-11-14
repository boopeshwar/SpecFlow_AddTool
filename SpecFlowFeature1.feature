Feature: AddTool
	Add A Tool with Caliper size 10"

@mytag
Scenario Outline: Add Tool
	Given User Login to the ERP portal
	And Click on the AddTool page
	When User enter the tool details with <tooltype> and <toolsize>
	And upload the pictures
	Then the tool should be added

	Examples:
	| tooltype       | toolsize |
	| RS CALIPER     | 12       |
	| RS DEFORMATION | 14       |
