The domain project is meant to only contain interfaces of the core domain of the solution and dumb data types that are part of the domain such as event args.
One of the reasons only the interfaces are kept in this project is because any project can implement these interfaces and those concrete types
can be registered in a common Inversion of Control Container and the types can be resolved by the interface name in any project. This solves module
dependency problems such as cylic references between projects.