# Well Design API
An exploration of a well designed API

## Notes

Some of the things I intend to do with this, should I find time, are as follows:

- I have almost finished a version of this using Dapper over Entity Framework. This aims to work generically (could be added and used for all entities then, meeting the open-closed principle)
- Refactor what is there so it works generically (to help it be used with all entities rather than just e.g. Movie)
- As the project was small, I didn't use migrations / a code-first approach to database creation. The SQLs for this are included in the repo
- I would have liked to have written the API using a TDD approach. This might be utilised for further work.
- Upgrade to .Net 8 over .Net 7
- Some deprecated packages used, need to update these
- Docker isn't configured fully as I have created this in Visual Studio (I would like to e.g. configure for db access, add volumes to bring e.g. the logs out of the container for easy access, docker-compose file not used as spinning up only one container for the API, add a Dockerfile for the publish config as well as the dev env)
- Implement a Repository pattern for EF
- Investigate the use of Graph QL for making a more maintainable / generically available / usable endpoint
- Some todos are littered throughout, e.g. apply validation to the existing search filter to ensure any magic strings resolve to property names


[![Build Status](https://travis-ci.org/username/repo.svg?branch=main)](https://travis-ci.org/username/repo)
[![Coverage Status](https://coveralls.io/repos/github/username/repo/badge.svg?branch=main)](https://coveralls.io/github/username/repo?branch=main)
![GitHub version](https://img.shields.io/badge/version-1.0-blue.svg)

## Table of Contents

- [Description](#description)
- [Installation](#installation)
- [Usage](#usage)
- [Configuration](#configuration)
- [Contributing](#contributing)
- [Testing](#testing)
- [Licence](#licence)
- [Acknowledgments](#acknowledgments)
- [Documentation](#documentation)
- [Contact Information](#contact-information)

## Description

EcomTest is an e-commerce API that provides a set of endpoints for managing orders, products, and customers.

## Installation

To install, follow these steps...

## Usage
..
## Configuration
..
## Contributing
..
## Testing
..
## Licence
--
## Acknowledgements
..
## Documentation
..
## Contact Information