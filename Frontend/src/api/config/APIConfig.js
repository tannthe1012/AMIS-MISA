var APIConfig = {
    development: 'https://localhost:44332',
    production: 'https://localhost:44332'
}

export default APIConfig[process.env.NODE_ENV];