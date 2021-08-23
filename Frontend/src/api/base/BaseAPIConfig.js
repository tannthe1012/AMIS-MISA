
import APIConfig from '../config/APIConfig';
import axios from 'axios';

/**
 * Thiết lập cơ bản cho API (base url, content type...)
 */
var BaseAPIConfig = axios.create({
    baseURL: APIConfig,
    headers: { 
      'Content-type': 'application/json' 
    },
});

export default BaseAPIConfig;