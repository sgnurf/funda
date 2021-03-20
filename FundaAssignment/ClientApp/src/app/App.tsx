import * as React from 'react';
import { Route } from 'react-router';
import Layout from './Layout';
import Home from '../features/Home/Home';

import './custom.css'

export default () => (
    <Layout>
        <Route exact path='/' component={Home} />
    </Layout>
);
