import * as React from 'react';
import { Route } from 'react-router';
import Layout from './Layout';
import './custom.css'
import TopAgents from '../features/topAgents/TopAgents';

export default () => (
    <Layout>
        <Route exact path='/' component={TopAgents} />
    </Layout>
);
