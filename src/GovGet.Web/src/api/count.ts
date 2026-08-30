import type { CountResult } from '../models/CountResult'

export async function getCount(): Promise<CountResult> {
  const response = await fetch('/api/count')

  if (!response.ok) {
    throw new Error(`Count failed with status ${response.status}`)
  }

  return response.json()
}
