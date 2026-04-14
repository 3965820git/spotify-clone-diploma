import next from 'eslint-config-next'
import prettier from 'eslint-plugin-prettier'
import prettierConfig from 'eslint-config-prettier'

export default [
  ...next,
  prettierConfig,
  {
    plugins: { prettier },
    rules: {
      'prettier/prettier': ['error', { semi: false }],
      semi: ['error', 'never'],
    },
  },
]
